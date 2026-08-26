using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransactionAggregationApi.Application.Domain.Queries;

namespace TransactionAggregationApi.Application.Controllers.v1;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/aggregations")]
public class AggregationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AggregationsController(IMediator mediator) => _mediator = mediator;

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary(CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetAggregationSummaryQuery(), cancellationToken));

    [HttpGet("client/{clientId}")]
    public async Task<IActionResult> GetClientAggregation(string clientId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetClientAggregationQuery(clientId), cancellationToken);
        return result.Success ? Ok(result) : NotFound();
    }

    [HttpGet("client/{clientId}/by-category")]
    public async Task<IActionResult> GetByCategory(string clientId, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetClientAggregationByCategoryQuery(clientId), cancellationToken));

    [HttpGet("client/{clientId}/by-source")]
    public async Task<IActionResult> GetBySource(string clientId, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetClientAggregationBySourceQuery(clientId), cancellationToken));

    [HttpGet("client/{clientId}/monthly")]
    public async Task<IActionResult> GetMonthly(string clientId, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetClientMonthlyAggregationQuery(clientId), cancellationToken));

    [HttpGet("top-spenders")]
    public async Task<IActionResult> GetTopSpenders([FromQuery] int top = 10, CancellationToken cancellationToken = default)
        => Ok(await _mediator.Send(new GetTopSpendersQuery(new Domain.Requests.GetTopSpendersRequest { TopN = top }), cancellationToken));
}
