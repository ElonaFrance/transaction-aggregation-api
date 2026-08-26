using MediatR;
using TransactionAggregationApi.Application.Domain.Commands;
using TransactionAggregationApi.Application.Domain.Responses;
using TransactionAggregationApi.Application.Services.Contracts;

namespace TransactionAggregationApi.Application.Handlers.Commands;

public class IngestTransactionHandler : IRequestHandler<IngestTransactionCommand, IngestResponse>
{
    private readonly ITransactionQueryService _service;
    public IngestTransactionHandler(ITransactionQueryService service) => _service = service;
    public Task<IngestResponse> Handle(IngestTransactionCommand request, CancellationToken cancellationToken)
        => _service.IngestTransactionAsync(request.Request, cancellationToken);
}
