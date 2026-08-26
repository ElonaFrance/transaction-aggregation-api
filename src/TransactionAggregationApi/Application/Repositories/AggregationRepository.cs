using System.Text.Json;
using Dapper;
using TransactionAggregationApi.Application.Domain.Constants;
using TransactionAggregationApi.Application.Domain.Entities;
using TransactionAggregationApi.Application.Repositories.Contracts;
using TransactionAggregationApi.Application.Services;

namespace TransactionAggregationApi.Application.Repositories;

public class AggregationRepository : IAggregationRepository
{
    private readonly DapperService _dapperService;

    public AggregationRepository(DapperService dapperService) => _dapperService = dapperService;

    public async Task<AggregationEntity?> FetchSummaryAsync(CancellationToken cancellationToken = default)
    {
        using var connection = _dapperService.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<AggregationEntity>(
            AppConstants.StoredProcedures.FetchAggregationSummary,
            new { Payload = "{}" },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<AggregationEntity?> FetchClientAggregationAsync(string clientId, CancellationToken cancellationToken = default)
    {
        using var connection = _dapperService.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<AggregationEntity>(
            AppConstants.StoredProcedures.FetchClientAggregation,
            new { Payload = JsonSerializer.Serialize(new { ClientId = clientId }) },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<dynamic>> FetchClientCategoryBreakdownAsync(string clientId, CancellationToken cancellationToken = default)
    {
        using var connection = _dapperService.CreateConnection();
        return await connection.QueryAsync(
            AppConstants.StoredProcedures.FetchClientAggregationByCategory,
            new { Payload = JsonSerializer.Serialize(new { ClientId = clientId }) },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<dynamic>> FetchClientSourceBreakdownAsync(string clientId, CancellationToken cancellationToken = default)
    {
        using var connection = _dapperService.CreateConnection();
        return await connection.QueryAsync(
            AppConstants.StoredProcedures.FetchClientAggregationBySource,
            new { Payload = JsonSerializer.Serialize(new { ClientId = clientId }) },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<AggregationEntity>> FetchClientMonthlyAsync(string clientId, CancellationToken cancellationToken = default)
    {
        using var connection = _dapperService.CreateConnection();
        return await connection.QueryAsync<AggregationEntity>(
            AppConstants.StoredProcedures.FetchClientMonthlyAggregation,
            new { Payload = JsonSerializer.Serialize(new { ClientId = clientId }) },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<dynamic>> FetchTopSpendersAsync(int top, CancellationToken cancellationToken = default)
    {
        using var connection = _dapperService.CreateConnection();
        return await connection.QueryAsync(
            AppConstants.StoredProcedures.FetchTopSpenders,
            new { Payload = JsonSerializer.Serialize(new { Top = top }) },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
