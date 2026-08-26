using MediatR;
using TransactionAggregationApi.Application.Domain.Queries;
using TransactionAggregationApi.Application.Domain.Responses;
using TransactionAggregationApi.Application.Services.Contracts;

namespace TransactionAggregationApi.Application.Handlers.Queries;

public class GetClientTransactionsQueryHandler : IRequestHandler<GetClientTransactionsQuery, PagedResponse<TransactionResponse>>
{
    private readonly ITransactionQueryService _service;
    public GetClientTransactionsQueryHandler(ITransactionQueryService service) => _service = service;
    public async Task<PagedResponse<TransactionResponse>> Handle(GetClientTransactionsQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetClientTransactionsAsync(request.ClientId, request.Request, cancellationToken);
        return PagedResponse<TransactionResponse>.Ok(result.Items, result.Page, result.PageSize, result.TotalCount);
    }
}
