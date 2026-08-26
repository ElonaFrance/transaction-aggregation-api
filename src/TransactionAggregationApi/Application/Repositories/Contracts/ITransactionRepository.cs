using TransactionAggregationApi.Application.Domain.Entities;
using TransactionAggregationApi.Application.Domain.Requests;

namespace TransactionAggregationApi.Application.Repositories.Contracts;

public interface ITransactionRepository
{
    Task<(IEnumerable<TransactionEntity> Items, int TotalCount)> FetchTransactionsAsync(GetTransactionsRequest request, CancellationToken cancellationToken = default);
    Task<TransactionEntity?> FetchByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<(IEnumerable<TransactionEntity> Items, int TotalCount)> FetchClientTransactionsAsync(string clientId, GetTransactionsRequest request, CancellationToken cancellationToken = default);
}
