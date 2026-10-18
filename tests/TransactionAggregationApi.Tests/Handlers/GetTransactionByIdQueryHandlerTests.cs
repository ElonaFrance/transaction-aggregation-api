using FluentAssertions;
using NSubstitute;
using Xunit;
using TransactionAggregationApi.Application.Domain.Queries;
using TransactionAggregationApi.Application.Domain.Responses;
using TransactionAggregationApi.Application.Handlers.Queries;
using TransactionAggregationApi.Application.Services.Contracts;

namespace TransactionAggregationApi.Tests.Handlers;

public class GetTransactionByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldDelegateToService()
    {
        var service = Substitute.For<ITransactionQueryService>();
        var transaction = new TransactionResponse { Id = "TX-001" };
        service.GetTransactionByIdAsync("TX-001", Arg.Any<CancellationToken>()).Returns(transaction);

        var handler = new GetTransactionByIdQueryHandler(service);
        var result = await handler.Handle(new GetTransactionByIdQuery("TX-001"), CancellationToken.None);

        result.Success.Should().BeTrue();
        result.Data.Should().BeSameAs(transaction);
        await service.Received(1).GetTransactionByIdAsync("TX-001", Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenNotFound_ShouldReturn404()
    {
        var service = Substitute.For<ITransactionQueryService>();
        service.GetTransactionByIdAsync("TX-404", Arg.Any<CancellationToken>()).Returns((TransactionResponse?)null);

        var handler = new GetTransactionByIdQueryHandler(service);
        var result = await handler.Handle(new GetTransactionByIdQuery("TX-404"), CancellationToken.None);

        result.Success.Should().BeFalse();
        result.ErrorCode.Should().Be(404);
    }
}
