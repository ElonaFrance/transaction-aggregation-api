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
            TotalTransactions = entity.TotalTransactionCount,
            UniqueClients = entity.UniqueClientCount,
            TopCategory = entity.TopCategory ?? string.Empty,
            TopSource = entity.TopSource ?? string.Empty
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
            CategoryId = r.CategoryId,
            CategoryName = r.CategoryName,
            TotalAmount = r.TotalAmount,
            TransactionCount = r.TransactionCount
        }).ToList();

        return new CategoryBreakdownResponse { ClientId = clientId, Breakdown = breakdown };
    }

    public async Task<SourceBreakdownResponse> GetClientSourceBreakdownAsync(string clientId, CancellationToken cancellationToken = default)
    {
        var rows = await _repository.FetchClientSourceBreakdownAsync(clientId, cancellationToken);

        var breakdown = rows.Select(r => new SourceBreakdownItem
        {
            SourceType = r.SourceType,
            TotalDebit = r.TotalDebit,
            TotalCredit = r.TotalCredit,
            TransactionCount = r.TransactionCount
        }).ToList();

        return new SourceBreakdownResponse { ClientId = clientId, Breakdown = breakdown };
    }

    public async Task<MonthlyAggregationResponse> GetClientMonthlyAggregationAsync(string clientId, CancellationToken cancellationToken = default)
    {
        var entities = await _repository.FetchClientMonthlyAsync(clientId, cancellationToken);

        var monthly = entities.Select(e => new MonthlyAggregationItem
        {
            PeriodKey = e.Period,
            TotalDebit = e.TotalDebit,
            TotalCredit = e.TotalCredit,
            TransactionCount = e.TransactionCount
        }).OrderBy(m => m.PeriodKey).ToList();

        return new MonthlyAggregationResponse { ClientId = clientId, Monthly = monthly };
    }

    public async Task<IEnumerable<TopSpenderResponse>> GetTopSpendersAsync(int top, CancellationToken cancellationToken = default)
    {
        var rows = await _repository.FetchTopSpendersAsync(top, cancellationToken);

        return rows.Select(r => new TopSpenderResponse
        {
            ClientId = r.ClientId,
            TotalDebit = r.TotalSpend,
            TransactionCount = r.TransactionCount,
            Rank = r.Rank
        }).ToList();
    }
}
