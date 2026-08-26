using TransactionAggregationApi.Application.Domain.Requests;
using TransactionAggregationApi.Application.Domain.Responses;

namespace TransactionAggregationApi.Application.Services.Contracts;

public interface ITransactionQueryService
{
    Task<PagedTransactionResponse> GetTransactionsAsync(GetTransactionsRequest request, CancellationToken cancellationToken = default);
    Task<TransactionResponse?> GetTransactionByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<PagedTransactionResponse> GetClientTransactionsAsync(string clientId, GetTransactionsRequest request, CancellationToken cancellationToken = default);
    Task<PagedTransactionResponse> GetCategoryTransactionsAsync(int categoryId, GetTransactionsRequest request, CancellationToken cancellationToken = default);
    Task<IngestResponse> IngestTransactionAsync(IngestTransactionRequest request, CancellationToken cancellationToken = default);
}
