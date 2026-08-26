using MediatR;
using TransactionAggregationApi.Application.Domain.Requests;
using TransactionAggregationApi.Application.Domain.Responses;

namespace TransactionAggregationApi.Application.Domain.Queries;

public record GetTopSpendersQuery(GetTopSpendersRequest Request) : IRequest<ApiResponse<IEnumerable<TopSpenderResponse>>>;
