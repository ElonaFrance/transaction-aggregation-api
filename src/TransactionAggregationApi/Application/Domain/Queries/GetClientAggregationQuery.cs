using MediatR;
using TransactionAggregationApi.Application.Domain.Responses;

namespace TransactionAggregationApi.Application.Domain.Queries;

public record GetClientAggregationQuery(string ClientId) : IRequest<ApiResponse<ClientAggregationResponse>>;
