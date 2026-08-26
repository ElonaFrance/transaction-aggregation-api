using MediatR;
using TransactionAggregationApi.Application.Domain.Responses;

namespace TransactionAggregationApi.Application.Domain.Queries;

public record GetClientAggregationBySourceQuery(string ClientId) : IRequest<ApiResponse<SourceBreakdownResponse>>;
