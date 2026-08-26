using MediatR;
using TransactionAggregationApi.Application.Domain.Queries;
using TransactionAggregationApi.Application.Domain.Responses;
using TransactionAggregationApi.Application.Services.Contracts;

namespace TransactionAggregationApi.Application.Handlers.Queries;

public class GetTopSpendersQueryHandler : IRequestHandler<GetTopSpendersQuery, ApiResponse<IEnumerable<TopSpenderResponse>>>
{
    private readonly IAggregationQueryService _service;
    public GetTopSpendersQueryHandler(IAggregationQueryService service) => _service = service;
    public async Task<ApiResponse<IEnumerable<TopSpenderResponse>>> Handle(GetTopSpendersQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetTopSpendersAsync(request.Request.TopN, cancellationToken);
        return ApiResponse<IEnumerable<TopSpenderResponse>>.Ok(result);
    }
}
