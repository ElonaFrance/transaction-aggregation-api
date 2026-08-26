using MediatR;
using TransactionAggregationApi.Application.Domain.Queries;
using TransactionAggregationApi.Application.Domain.Responses;
using TransactionAggregationApi.Application.Services.Contracts;

namespace TransactionAggregationApi.Application.Handlers.Queries;

public class GetClientAggregationByCategoryQueryHandler : IRequestHandler<GetClientAggregationByCategoryQuery, ApiResponse<CategoryBreakdownResponse>>
{
    private readonly IAggregationQueryService _service;
    public GetClientAggregationByCategoryQueryHandler(IAggregationQueryService service) => _service = service;
    public async Task<ApiResponse<CategoryBreakdownResponse>> Handle(GetClientAggregationByCategoryQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetClientCategoryBreakdownAsync(request.ClientId, cancellationToken);
        return ApiResponse<CategoryBreakdownResponse>.Ok(result);
    }
}
