using FluentAssertions;
using NSubstitute;
using TransactionAggregationApi.Application.Domain.Entities;
using TransactionAggregationApi.Application.Domain.Requests;
using TransactionAggregationApi.Application.Repositories.Contracts;
using TransactionAggregationApi.Application.Services;

namespace TransactionAggregationApi.Tests.Services;

public class TransactionQueryServiceTests
{
    private readonly ITransactionRepository _repository = Substitute.For<ITransactionRepository>();
    private readonly KafkaProducerService _producer = Substitute.For<KafkaProducerService>();
    private readonly Microsoft.Extensions.Logging.ILogger<TransactionQueryService> _logger =
        Substitute.For<Microsoft.Extensions.Logging.ILogger<TransactionQueryService>>();

    private TransactionQueryService CreateSut() => new(_repository, _producer, _logger);

    [Fact]
    public async Task GetTransactionsAsync_ShouldMapResultsCorrectly()
    {
        var request = new GetTransactionsRequest { Page = 1, PageSize = 10 };
        var entities = new List<TransactionEntity>
        {
            new() { Id = "TXN-001", ClientId = "CLIENT-001", Amount = 500, CategoryName = "Groceries", TransactionType = "Debit" }
        };
        _repository.FetchTransactionsAsync(request).Returns((entities, 1));

        var result = await CreateSut().GetTransactionsAsync(request);

        result.Items.Should().HaveCount(1);
        result.TotalCount.Should().Be(1);
        result.Items.First().Id.Should().Be("TXN-001");
    }

    [Fact]
    public async Task GetTransactionByIdAsync_WhenNotFound_ShouldReturnNull()
    {
        _repository.FetchByIdAsync("NONEXISTENT").Returns((TransactionEntity?)null);

        var result = await CreateSut().GetTransactionByIdAsync("NONEXISTENT");

        result.Should().BeNull();
    }
}
