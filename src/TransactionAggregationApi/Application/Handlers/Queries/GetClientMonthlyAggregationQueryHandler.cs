using MediatR;
using TransactionAggregationApi.Application.Domain.Queries;
using TransactionAggregationApi.Application.Domain.Responses;
using TransactionAggregationApi.Application.Services.Contracts;

namespace TransactionAggregationApi.Application.Handlers.Queries;

public class GetClientMonthlyAggregationQueryHandler : IRequestHandler<GetClientMonthlyAggregationQuery, ApiResponse<MonthlyAggregationResponse>>
{
    private readonly IAggregationQueryService _service;
    public GetClientMonthlyAggregationQueryHandler(IAggregationQueryService service) => _service = service;
    public async Task<ApiResponse<MonthlyAggregationResponse>> Handle(GetClientMonthlyAggregationQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetClientMonthlyAggregationAsync(request.ClientId, cancellationToken);
        return ApiResponse<MonthlyAggregationResponse>.Ok(result);
    }
}
