using System.Text.Json;
using Dapper;
using TransactionAggregationApi.Application.Domain.Constants;
using TransactionAggregationApi.Application.Domain.Entities;
using TransactionAggregationApi.Application.Domain.Requests;
using TransactionAggregationApi.Application.Repositories.Contracts;
using TransactionAggregationApi.Application.Services;

namespace TransactionAggregationApi.Application.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly DapperService _dapperService;

    public TransactionRepository(DapperService dapperService) => _dapperService = dapperService;

    public async Task<(IEnumerable<TransactionEntity> Items, int TotalCount)> FetchTransactionsAsync(
        GetTransactionsRequest request, CancellationToken cancellationToken = default)
    {
        using var connection = _dapperService.CreateConnection();
        var payload = JsonSerializer.Serialize(request);

        using var multi = await connection.QueryMultipleAsync(
            AppConstants.StoredProcedures.FetchTransactions,
            new { Payload = payload },
            commandType: System.Data.CommandType.StoredProcedure);

        var items = await multi.ReadAsync<TransactionEntity>();
        var total = await multi.ReadFirstOrDefaultAsync<int>();
        return (items, total);
    }

    public async Task<TransactionEntity?> FetchByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        using var connection = _dapperService.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<TransactionEntity>(
            AppConstants.StoredProcedures.FetchTransactionById,
            new { Id = id },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<(IEnumerable<TransactionEntity> Items, int TotalCount)> FetchClientTransactionsAsync(
        string clientId, GetTransactionsRequest request, CancellationToken cancellationToken = default)
    {
        using var connection = _dapperService.CreateConnection();
        var payload = JsonSerializer.Serialize(request);

        using var multi = await connection.QueryMultipleAsync(
            AppConstants.StoredProcedures.FetchClientTransactions,
            new { ClientId = clientId, Payload = payload },
            commandType: System.Data.CommandType.StoredProcedure);

        var items = await multi.ReadAsync<TransactionEntity>();
        var total = await multi.ReadFirstOrDefaultAsync<int>();
        return (items, total);
    }
}
