using MediatR;
using TransactionAggregationApi.Application.Domain.Queries;
using TransactionAggregationApi.Application.Domain.Responses;
using TransactionAggregationApi.Application.Services.Contracts;

namespace TransactionAggregationApi.Application.Handlers.Queries;

public class GetAggregationSummaryQueryHandler : IRequestHandler<GetAggregationSummaryQuery, ApiResponse<AggregationSummaryResponse>>
{
    private readonly IAggregationQueryService _service;
    public GetAggregationSummaryQueryHandler(IAggregationQueryService service) => _service = service;
    public async Task<ApiResponse<AggregationSummaryResponse>> Handle(GetAggregationSummaryQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetSummaryAsync(cancellationToken);
        return ApiResponse<AggregationSummaryResponse>.Ok(result);
    }
}
