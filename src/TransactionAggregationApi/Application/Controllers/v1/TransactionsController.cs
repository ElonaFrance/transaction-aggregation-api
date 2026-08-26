using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransactionAggregationApi.Application.Domain.Commands;
using TransactionAggregationApi.Application.Domain.Queries;
using TransactionAggregationApi.Application.Domain.Requests;

namespace TransactionAggregationApi.Application.Controllers.v1;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetTransactions([FromQuery] GetTransactionsRequest request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetTransactionsQuery(request), cancellationToken));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTransactionByIdQuery(id), cancellationToken);
        return result.Success ? Ok(result) : NotFound();
    }

    [HttpGet("client/{clientId}")]
    public async Task<IActionResult> GetClientTransactions(string clientId, [FromQuery] GetTransactionsRequest request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetClientTransactionsQuery(clientId, request), cancellationToken));

    [HttpPost("ingest")]
    public async Task<IActionResult> Ingest([FromBody] IngestTransactionRequest request, CancellationToken cancellationToken)
        => Accepted(await _mediator.Send(new IngestTransactionCommand(request), cancellationToken));
}
