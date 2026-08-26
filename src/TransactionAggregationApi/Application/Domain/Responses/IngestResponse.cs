namespace TransactionAggregationApi.Application.Domain.Responses;

public class IngestResponse
{
    public string TransactionId { get; set; } = string.Empty;
    public string EventId { get; set; } = string.Empty;
    public string Status { get; set; } = "PUBLISHED";
    public DateTime PublishedAt { get; set; }
}
