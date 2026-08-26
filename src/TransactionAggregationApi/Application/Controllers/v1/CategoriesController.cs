using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransactionAggregationApi.Application.Domain.Queries;
using TransactionAggregationApi.Application.Domain.Requests;

namespace TransactionAggregationApi.Application.Controllers.v1;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/categories")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetCategoriesQuery(), cancellationToken));

    [HttpGet("{categoryId}/transactions")]
    public async Task<IActionResult> GetCategoryTransactions(int categoryId, [FromQuery] GetTransactionsRequest request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetCategoryTransactionsQuery(categoryId, request), cancellationToken));
}
