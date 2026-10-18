using TransactionAggregationApi.Application.Domain.Entities;

namespace TransactionAggregationApi.Application.Repositories.Contracts;

public interface IAggregationRepository
{
    Task<AggregationSummaryEntity?> FetchSummaryAsync(CancellationToken cancellationToken = default);
    Task<ClientAggregationEntity?> FetchClientAggregationAsync(string clientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<CategoryBreakdownEntity>> FetchClientCategoryBreakdownAsync(string clientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<SourceBreakdownEntity>> FetchClientSourceBreakdownAsync(string clientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<MonthlyAggregationEntity>> FetchClientMonthlyAsync(string clientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TopSpenderEntity>> FetchTopSpendersAsync(int top, CancellationToken cancellationToken = default);
}
