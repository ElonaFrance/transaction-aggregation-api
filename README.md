# Transaction Aggregation API

Read-only REST API for aggregated customer transaction data. Part of a two-service event-driven system built in **C# / .NET 8**.

## Architecture

```
Mock Sources → Kafka (Redpanda) → Transaction Processor → MySQL
                                                            ↑
                                   Transaction Aggregation API (this service)
```

The **Transaction Processor** (`transaction-processor`) ingests mock data from three sources (BankAccount, CreditCard, Investment), categorises each transaction using a 15-category keyword rules engine, and persists both raw transactions and pre-computed aggregations to MySQL.

This API queries the pre-aggregated data — it never touches Kafka.

## Tech Stack

| Concern | Choice |
|---------|--------|
| Language | C# 12 / .NET 8 |
| API pattern | CQRS + MediatR 12.x |
| ORM | Dapper + MySQL stored procedures |
| Auth | JWT Bearer (HS256) |
| Validation | FluentValidation 11.x |
| Mapping | AutoMapper 13.x |
| Docs | Swagger UI (Swashbuckle 6.x) |
| Logging | Serilog → Console (structured JSON) |
| Tests | xUnit + NSubstitute + FluentAssertions |

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (for full stack)

---

## Running the Full Stack (Docker Compose)

This `docker-compose.yml` orchestrates both services + MySQL + Redpanda.

```bash
# From this directory:
docker compose up --build
```

Services started:
| Service | Port | URL |
|---------|------|-----|
| Transaction Aggregation API | 8081 | http://localhost:8081 |
| Transaction Processor | 8080 | http://localhost:8080 |
| MySQL | 3306 | localhost:3306 |
| Redpanda (Kafka) | 19092 | localhost:19092 |

Swagger UI: http://localhost:8081/swagger

> The Transaction Processor begins generating and publishing mock transactions every 5 seconds automatically on startup.

---

## Running the API Locally (without Docker)

```bash
# 1. Start MySQL and Redpanda (or point to existing instances)
# 2. Apply database schema
mysql -u root -p < ../transaction-processor/database/01_schema.sql
mysql -u root -p transaction_db < ../transaction-processor/database/02_stored_procedures.sql

# 3. Start the API
cd src/TransactionAggregationApi
dotnet run
```

The API will be available at http://localhost:8081.

---

## Authentication

All endpoints require a JWT Bearer token.

**Generating a token for local testing** — use [jwt.io](https://jwt.io) with:

```
Algorithm: HS256
Secret:    TransactionAggregationApi-SuperSecretKey-2024-DoNotUseInProduction
Payload:
{
  "sub": "test-user",
  "iss": "transaction-aggregation-api",
  "aud": "transaction-aggregation-clients",
  "exp": <unix timestamp 1 hour from now>
}
```

Pass it as: `Authorization: Bearer <token>`

---

## API Endpoints

### Transactions

| Method | Path | Description |
|--------|------|-------------|
| `GET` | `/api/v1/transactions` | List transactions (filterable, paginated) |
| `GET` | `/api/v1/transactions/{id}` | Get transaction by ID |
| `GET` | `/api/v1/transactions/client/{clientId}` | Get transactions for a client |

**Query parameters for list endpoints:**
`clientId`, `sourceType`, `category`, `transactionType`, `minAmount`, `maxAmount`, `fromDate`, `toDate`, `page`, `pageSize`, `sortBy`, `sortDirection`

### Aggregations

| Method | Path | Description |
|--------|------|-------------|
| `GET` | `/api/v1/aggregations/summary` | Overall system summary |
| `GET` | `/api/v1/aggregations/client/{clientId}` | Client totals |
| `GET` | `/api/v1/aggregations/client/{clientId}/by-category` | Client spending by category |
| `GET` | `/api/v1/aggregations/client/{clientId}/by-source` | Client spending by source |
| `GET` | `/api/v1/aggregations/client/{clientId}/monthly` | Client monthly breakdown |
| `GET` | `/api/v1/aggregations/top-spenders` | Top spenders (default top 10) |

### Categories

| Method | Path | Description |
|--------|------|-------------|
| `GET` | `/api/v1/categories` | List all 15 categories |
| `GET` | `/api/v1/categories/{categoryId}/transactions` | Transactions in a category |

### Health

| Method | Path | Description |
|--------|------|-------------|
| `GET` | `/health/liveness` | Service alive |
| `GET` | `/health/readiness` | Service + MySQL ready |

---

## Running Tests

```bash
dotnet test tests/TransactionAggregationApi.Tests/
```

---

## Project Structure

```
src/TransactionAggregationApi/
├── Application/
│   ├── Controllers/v1/          # HTTP endpoints
│   ├── Domain/
│   │   ├── Entities/            # DB models
│   │   ├── Queries/             # MediatR query records
│   │   ├── Requests/            # HTTP request models
│   │   └── Responses/           # HTTP response DTOs
│   ├── Handlers/Queries/        # Thin MediatR handlers
│   ├── Repositories/            # Dapper + stored procedure calls
│   └── Services/                # Business logic (heavy lifting)
└── Core/
    ├── AutoMapper/Profiles/
    ├── Extensions/              # DI registrations
    ├── FluentValidators/
    ├── Middleware/              # Exception handler
    └── Swagger/
tests/TransactionAggregationApi.Tests/
```

---

## GitHub

- **Transaction Aggregation API**: https://github.com/ElonaFrance/transaction-aggregation-api
- **Transaction Processor**: https://github.com/ElonaFrance/transaction-processor

**Language:** C# / .NET 8 | **Area:** Backend
