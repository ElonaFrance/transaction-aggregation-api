using TransactionAggregationApi.Application.Domain.Constants;
using TransactionAggregationApi.Application.Domain.Events;
using TransactionAggregationApi.Application.Domain.Requests;
using TransactionAggregationApi.Application.Domain.Responses;
using TransactionAggregationApi.Application.Repositories.Contracts;
using TransactionAggregationApi.Application.Services.Contracts;

namespace TransactionAggregationApi.Application.Services;

public class TransactionQueryService : ITransactionQueryService
{
    private readonly ITransactionRepository _repository;
    private readonly KafkaProducerService _producer;
    private readonly ILogger<TransactionQueryService> _logger;

    public TransactionQueryService(
        ITransactionRepository repository,
        KafkaProducerService producer,
        ILogger<TransactionQueryService> logger)
    {
        _repository = repository;
        _producer = producer;
        _logger = logger;
    }

    public async Task<PagedTransactionResponse> GetTransactionsAsync(GetTransactionsRequest request, CancellationToken cancellationToken = default)
    {
        var (items, total) = await _repository.FetchTransactionsAsync(request, cancellationToken);
        return new PagedTransactionResponse
        {
            Items = items.Select(Map),
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = total
        };
    }

    public async Task<TransactionResponse?> GetTransactionByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.FetchByIdAsync(id, cancellationToken);
        return entity == null ? null : Map(entity);
    }

    public async Task<PagedTransactionResponse> GetClientTransactionsAsync(string clientId, GetTransactionsRequest request, CancellationToken cancellationToken = default)
    {
        var (items, total) = await _repository.FetchClientTransactionsAsync(clientId, request, cancellationToken);
        return new PagedTransactionResponse
        {
            Items = items.Select(Map),
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = total
        };
    }

    public async Task<PagedTransactionResponse> GetCategoryTransactionsAsync(int categoryId, GetTransactionsRequest request, CancellationToken cancellationToken = default)
    {
        request.CategoryId = categoryId;
        return await GetTransactionsAsync(request, cancellationToken);
    }

    public async Task<IngestResponse> IngestTransactionAsync(IngestTransactionRequest request, CancellationToken cancellationToken = default)
    {
        var evt = new TransactionEvent
        {
            TransactionId = Guid.NewGuid().ToString(),
            ClientId = request.ClientId,
            SourceId = $"{request.SourceType.ToUpper()}-{request.ClientId}",
            SourceType = request.SourceType,
            TransactionType = request.TransactionType,
            Amount = request.Amount,
            Currency = request.Currency,
            Description = request.Description,
            MerchantName = request.MerchantName,
            TransactionDate = request.TransactionDate,
            PublishedAt = DateTime.UtcNow
        };

        await _producer.ProduceAsync(AppConstants.Kafka.TransactionEventsTopic, evt.ClientId, evt, cancellationToken);
        _logger.LogInformation("Ingested transaction {TransactionId} for client {ClientId}", evt.TransactionId, evt.ClientId);

        return new IngestResponse
        {
            TransactionId = evt.TransactionId,
            EventId = evt.EventId,
            Status = "PUBLISHED",
            PublishedAt = evt.PublishedAt
        };
    }

    private static TransactionResponse Map(Domain.Entities.TransactionEntity e) => new()
    {
        Id = e.Id,
        ClientId = e.ClientId,
        SourceId = e.SourceId,
        SourceType = e.SourceType,
        TransactionType = e.TransactionType,
        Amount = e.Amount,
        Currency = e.Currency,
        CategoryId = e.CategoryId,
        CategoryName = e.CategoryName,
        Description = e.Description,
        MerchantName = e.MerchantName,
        TransactionDate = e.TransactionDate,
        ProcessedDate = e.ProcessedDate,
        Status = e.Status
    };
}
