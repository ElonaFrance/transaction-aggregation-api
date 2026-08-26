using MediatR;
using TransactionAggregationApi.Application.Domain.Responses;

namespace TransactionAggregationApi.Application.Domain.Queries;

public record GetAggregationSummaryQuery : IRequest<ApiResponse<AggregationSummaryResponse>>;
