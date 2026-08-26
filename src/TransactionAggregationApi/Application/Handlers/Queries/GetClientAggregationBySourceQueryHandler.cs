using MediatR;
using TransactionAggregationApi.Application.Domain.Queries;
using TransactionAggregationApi.Application.Domain.Responses;
using TransactionAggregationApi.Application.Services.Contracts;

namespace TransactionAggregationApi.Application.Handlers.Queries;

public class GetClientAggregationBySourceQueryHandler : IRequestHandler<GetClientAggregationBySourceQuery, ApiResponse<SourceBreakdownResponse>>
{
    private readonly IAggregationQueryService _service;
    public GetClientAggregationBySourceQueryHandler(IAggregationQueryService service) => _service = service;
    public async Task<ApiResponse<SourceBreakdownResponse>> Handle(GetClientAggregationBySourceQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetClientSourceBreakdownAsync(request.ClientId, cancellationToken);
        return ApiResponse<SourceBreakdownResponse>.Ok(result);
    }
}
