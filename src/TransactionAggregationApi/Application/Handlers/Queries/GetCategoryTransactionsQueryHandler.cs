using MediatR;
using TransactionAggregationApi.Application.Domain.Queries;
using TransactionAggregationApi.Application.Domain.Responses;
using TransactionAggregationApi.Application.Services.Contracts;

namespace TransactionAggregationApi.Application.Handlers.Queries;

public class GetCategoryTransactionsQueryHandler : IRequestHandler<GetCategoryTransactionsQuery, PagedResponse<TransactionResponse>>
{
    private readonly ITransactionQueryService _service;
    public GetCategoryTransactionsQueryHandler(ITransactionQueryService service) => _service = service;
    public async Task<PagedResponse<TransactionResponse>> Handle(GetCategoryTransactionsQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetCategoryTransactionsAsync(request.CategoryId, request.Request, cancellationToken);
        return PagedResponse<TransactionResponse>.Ok(result.Items, result.Page, result.PageSize, result.TotalCount);
    }
}
