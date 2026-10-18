FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY src/TransactionAggregationApi/TransactionAggregationApi.csproj ./src/TransactionAggregationApi/
RUN dotnet restore ./src/TransactionAggregationApi/TransactionAggregationApi.csproj

COPY src/ ./src/
RUN dotnet publish ./src/TransactionAggregationApi/TransactionAggregationApi.csproj \
    -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
# curl is used by the docker-compose healthcheck.
RUN apt-get update \
    && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
EXPOSE 8081
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "transaction-aggregation-api.dll"]
