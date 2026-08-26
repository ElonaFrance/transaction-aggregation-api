using TransactionAggregationApi.Application.Domain.Responses;

namespace TransactionAggregationApi.Application.Services.Contracts;

public interface IAggregationQueryService
{
    Task<AggregationSummaryResponse> GetSummaryAsync(CancellationToken cancellationToken = default);
    Task<ClientAggregationResponse?> GetClientAggregationAsync(string clientId, CancellationToken cancellationToken = default);
    Task<CategoryBreakdownResponse> GetClientCategoryBreakdownAsync(string clientId, CancellationToken cancellationToken = default);
    Task<SourceBreakdownResponse> GetClientSourceBreakdownAsync(string clientId, CancellationToken cancellationToken = default);
    Task<MonthlyAggregationResponse> GetClientMonthlyAggregationAsync(string clientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TopSpenderResponse>> GetTopSpendersAsync(int top, CancellationToken cancellationToken = default);
}
