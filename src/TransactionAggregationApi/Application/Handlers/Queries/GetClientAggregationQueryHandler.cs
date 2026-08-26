using MediatR;
using TransactionAggregationApi.Application.Domain.Queries;
using TransactionAggregationApi.Application.Domain.Responses;
using TransactionAggregationApi.Application.Services.Contracts;

namespace TransactionAggregationApi.Application.Handlers.Queries;

public class GetClientAggregationQueryHandler : IRequestHandler<GetClientAggregationQuery, ApiResponse<ClientAggregationResponse>>
{
    private readonly IAggregationQueryService _service;
    public GetClientAggregationQueryHandler(IAggregationQueryService service) => _service = service;
    public async Task<ApiResponse<ClientAggregationResponse>> Handle(GetClientAggregationQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetClientAggregationAsync(request.ClientId, cancellationToken);
        return result == null
            ? ApiResponse<ClientAggregationResponse>.Fail("Not found", 404)
            : ApiResponse<ClientAggregationResponse>.Ok(result);
    }
}
