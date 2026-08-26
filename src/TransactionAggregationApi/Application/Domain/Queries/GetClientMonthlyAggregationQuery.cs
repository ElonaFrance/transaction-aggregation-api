using MediatR;
using TransactionAggregationApi.Application.Domain.Responses;

namespace TransactionAggregationApi.Application.Domain.Queries;

public record GetClientMonthlyAggregationQuery(string ClientId) : IRequest<ApiResponse<MonthlyAggregationResponse>>;
