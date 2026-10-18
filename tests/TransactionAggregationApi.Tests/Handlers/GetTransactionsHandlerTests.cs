using FluentAssertions;
using NSubstitute;
using Xunit;
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
        _service.GetTransactionsAsync(request, Arg.Any<CancellationToken>()).Returns(expected);

        var handler = new GetTransactionsQueryHandler(_service);
        var result = await handler.Handle(new GetTransactionsQuery(request), CancellationToken.None);

        result.Success.Should().BeTrue();
        result.TotalCount.Should().Be(5);
        result.Page.Should().Be(1);
        result.PageSize.Should().Be(20);
        await _service.Received(1).GetTransactionsAsync(request, Arg.Any<CancellationToken>());
    }
}
