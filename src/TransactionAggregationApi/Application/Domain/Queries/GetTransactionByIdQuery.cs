using MediatR;
using TransactionAggregationApi.Application.Domain.Responses;

namespace TransactionAggregationApi.Application.Domain.Queries;

public record GetTransactionByIdQuery(string Id) : IRequest<ApiResponse<TransactionResponse>>;
