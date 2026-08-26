using FluentAssertions;
using NSubstitute;
using TransactionAggregationApi.Application.Domain.Queries;
using TransactionAggregationApi.Application.Domain.Requests;
using TransactionAggregationApi.Application.Domain.Responses;
using TransactionAggregationApi.Application.Handlers.Queries;
using TransactionAggregationApi.Application.Services.Contracts;

namespace TransactionAggregationApi.Tests.Handlers;

public class GetTransactionsHandlerTests
{
    private readonly ITransactionQueryService _service = Substitute.For<ITransactionQueryService>();

    [Fact]
    public async Task Handle_ShouldDelegateToService()
    {
        var request = new GetTransactionsRequest();
        var expected = new PagedTransactionResponse { TotalCount = 5, Page = 1, PageSize = 20 };
        _service.GetTransactionsAsync(request).Returns(expected);

        var handler = new GetTransactionsHandler(_service);
        var result = await handler.Handle(new GetTransactionsQuery(request), CancellationToken.None);

        result.Should().BeEquivalentTo(expected);
        await _service.Received(1).GetTransactionsAsync(request);
    }
}
