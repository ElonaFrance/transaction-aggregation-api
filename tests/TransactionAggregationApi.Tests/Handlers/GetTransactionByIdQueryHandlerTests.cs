using FluentAssertions;
using NSubstitute;
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
        var expected = ApiResponse<TransactionResponse>.Ok(new TransactionResponse { Id = "TX-001" });
        service.GetTransactionByIdAsync("TX-001").Returns(expected);

        var handler = new GetTransactionByIdQueryHandler(service);
        var result = await handler.Handle(new GetTransactionByIdQuery("TX-001"), CancellationToken.None);

        result.Should().BeEquivalentTo(expected);
        await service.Received(1).GetTransactionByIdAsync("TX-001");
    }
}
