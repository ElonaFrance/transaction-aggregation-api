using TransactionAggregationApi.Application.Domain.Responses;
using TransactionAggregationApi.Application.Repositories.Contracts;
using TransactionAggregationApi.Application.Services.Contracts;

namespace TransactionAggregationApi.Application.Services;

public class AggregationQueryService : IAggregationQueryService
{
    private readonly IAggregationRepository _repository;

    public AggregationQueryService(IAggregationRepository repository) => _repository = repository;

    public async Task<AggregationSummaryResponse> GetSummaryAsync(CancellationToken cancellationToken = default)
    {
        var entity = await _repository.FetchSummaryAsync(cancellationToken);
        if (entity == null) return new AggregationSummaryResponse();

        return new AggregationSummaryResponse
        {
            TotalDebit = entity.TotalDebit,
            TotalCredit = entity.TotalCredit,
            TotalTransactions = entity.TransactionCount
        };
    }

    public async Task<ClientAggregationResponse?> GetClientAggregationAsync(string clientId, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.FetchClientAggregationAsync(clientId, cancellationToken);
        if (entity == null) return null;

        return new ClientAggregationResponse
        {
            ClientId = clientId,
            TotalDebit = entity.TotalDebit,
            TotalCredit = entity.TotalCredit,
            TransactionCount = entity.TransactionCount,
            LastUpdated = entity.LastUpdated
        };
    }

    public async Task<CategoryBreakdownResponse> GetClientCategoryBreakdownAsync(string clientId, CancellationToken cancellationToken = default)
    {
        var rows = await _repository.FetchClientCategoryBreakdownAsync(clientId, cancellationToken);

        var breakdown = rows.Select(r => new CategoryBreakdownItem
        {
            CategoryId = (int)(r.CategoryId ?? 0),
            CategoryName = (string)(r.CategoryName ?? "Unknown"),
            TotalAmount = (decimal)(r.TotalAmount ?? 0m),
            TransactionCount = (int)(r.TransactionCount ?? 0)
        }).ToList();

        return new CategoryBreakdownResponse { ClientId = clientId, Breakdown = breakdown };
    }

    public async Task<SourceBreakdownResponse> GetClientSourceBreakdownAsync(string clientId, CancellationToken cancellationToken = default)
    {
        var rows = await _repository.FetchClientSourceBreakdownAsync(clientId, cancellationToken);

        var breakdown = rows.Select(r => new SourceBreakdownItem
        {
            SourceType = (string)(r.SourceType ?? "Unknown"),
            TotalDebit = (decimal)(r.TotalDebit ?? 0m),
            TotalCredit = (decimal)(r.TotalCredit ?? 0m),
            TransactionCount = (int)(r.TransactionCount ?? 0)
        }).ToList();

        return new SourceBreakdownResponse { ClientId = clientId, Breakdown = breakdown };
    }

    public async Task<MonthlyAggregationResponse> GetClientMonthlyAggregationAsync(string clientId, CancellationToken cancellationToken = default)
    {
        var entities = await _repository.FetchClientMonthlyAsync(clientId, cancellationToken);

        var monthly = entities.Select(e => new MonthlyAggregationItem
        {
            PeriodKey = e.PeriodKey,
            TotalDebit = e.TotalDebit,
            TotalCredit = e.TotalCredit,
            TransactionCount = e.TransactionCount
        }).OrderBy(m => m.PeriodKey).ToList();

        return new MonthlyAggregationResponse { ClientId = clientId, Monthly = monthly };
    }

    public async Task<IEnumerable<TopSpenderResponse>> GetTopSpendersAsync(int top, CancellationToken cancellationToken = default)
    {
        var rows = await _repository.FetchTopSpendersAsync(top, cancellationToken);

        return rows.Select((r, i) => new TopSpenderResponse
        {
            ClientId = (string)(r.ClientId ?? ""),
            TotalDebit = (decimal)(r.TotalDebit ?? 0m),
            TransactionCount = (int)(r.TransactionCount ?? 0),
            Rank = i + 1
        }).ToList();
    }
}
