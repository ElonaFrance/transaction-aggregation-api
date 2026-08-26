using MediatR;
using TransactionAggregationApi.Application.Domain.Requests;
using TransactionAggregationApi.Application.Domain.Responses;

namespace TransactionAggregationApi.Application.Domain.Queries;

public record GetTransactionsQuery(GetTransactionsRequest Request) : IRequest<PagedResponse<TransactionResponse>>;
