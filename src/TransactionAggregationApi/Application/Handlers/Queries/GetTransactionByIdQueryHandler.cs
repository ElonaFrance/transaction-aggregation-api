using MediatR;
using TransactionAggregationApi.Application.Domain.Queries;
using TransactionAggregationApi.Application.Domain.Responses;
using TransactionAggregationApi.Application.Services.Contracts;

namespace TransactionAggregationApi.Application.Handlers.Queries;

public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, ApiResponse<TransactionResponse>>
{
    private readonly ITransactionQueryService _service;
    public GetTransactionByIdQueryHandler(ITransactionQueryService service) => _service = service;
    public async Task<ApiResponse<TransactionResponse>> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetTransactionByIdAsync(request.Id, cancellationToken);
        return result == null
            ? ApiResponse<TransactionResponse>.Fail("Not found", 404)
            : ApiResponse<TransactionResponse>.Ok(result);
    }
}
