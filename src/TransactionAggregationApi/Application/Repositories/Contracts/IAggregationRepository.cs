using TransactionAggregationApi.Application.Domain.Entities;

namespace TransactionAggregationApi.Application.Repositories.Contracts;

public interface IAggregationRepository
{
    Task<AggregationEntity?> FetchSummaryAsync(CancellationToken cancellationToken = default);
    Task<AggregationEntity?> FetchClientAggregationAsync(string clientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<dynamic>> FetchClientCategoryBreakdownAsync(string clientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<dynamic>> FetchClientSourceBreakdownAsync(string clientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<AggregationEntity>> FetchClientMonthlyAsync(string clientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<dynamic>> FetchTopSpendersAsync(int top, CancellationToken cancellationToken = default);
}
