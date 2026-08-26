using MediatR;
using TransactionAggregationApi.Application.Domain.Requests;
using TransactionAggregationApi.Application.Domain.Responses;

namespace TransactionAggregationApi.Application.Domain.Commands;

public record IngestTransactionCommand(IngestTransactionRequest Request) : IRequest<IngestResponse>;
