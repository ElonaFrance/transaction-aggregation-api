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

    public async Task<AggregationSummaryEntity?> FetchSummaryAsync(CancellationToken cancellationToken = default)
    {
        // usp_FetchAggregationSummary takes no parameters.
        using var connection = _dapperService.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<AggregationSummaryEntity>(
            AppConstants.StoredProcedures.FetchAggregationSummary,
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<ClientAggregationEntity?> FetchClientAggregationAsync(string clientId, CancellationToken cancellationToken = default)
    {
        using var connection = _dapperService.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<ClientAggregationEntity>(
            AppConstants.StoredProcedures.FetchClientAggregation,
            new { Payload = JsonSerializer.Serialize(new { ClientId = clientId }) },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<CategoryBreakdownEntity>> FetchClientCategoryBreakdownAsync(string clientId, CancellationToken cancellationToken = default)
    {
        using var connection = _dapperService.CreateConnection();
        return await connection.QueryAsync<CategoryBreakdownEntity>(
            AppConstants.StoredProcedures.FetchClientAggregationByCategory,
            new { Payload = JsonSerializer.Serialize(new { ClientId = clientId }) },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<SourceBreakdownEntity>> FetchClientSourceBreakdownAsync(string clientId, CancellationToken cancellationToken = default)
    {
        using var connection = _dapperService.CreateConnection();
        return await connection.QueryAsync<SourceBreakdownEntity>(
            AppConstants.StoredProcedures.FetchClientAggregationBySource,
            new { Payload = JsonSerializer.Serialize(new { ClientId = clientId }) },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<MonthlyAggregationEntity>> FetchClientMonthlyAsync(string clientId, CancellationToken cancellationToken = default)
    {
        using var connection = _dapperService.CreateConnection();
        return await connection.QueryAsync<MonthlyAggregationEntity>(
            AppConstants.StoredProcedures.FetchClientMonthlyAggregation,
            new { Payload = JsonSerializer.Serialize(new { ClientId = clientId }) },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<TopSpenderEntity>> FetchTopSpendersAsync(int top, CancellationToken cancellationToken = default)
    {
        using var connection = _dapperService.CreateConnection();
        return await connection.QueryAsync<TopSpenderEntity>(
            AppConstants.StoredProcedures.FetchTopSpenders,
            new { Payload = JsonSerializer.Serialize(new { TopN = top }) },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
