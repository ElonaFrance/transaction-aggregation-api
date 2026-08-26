using MediatR;
using TransactionAggregationApi.Application.Domain.Responses;

namespace TransactionAggregationApi.Application.Domain.Queries;

public record GetClientAggregationByCategoryQuery(string ClientId) : IRequest<ApiResponse<CategoryBreakdownResponse>>;
