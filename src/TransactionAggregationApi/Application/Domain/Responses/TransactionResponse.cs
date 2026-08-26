namespace TransactionAggregationApi.Application.Domain.Responses;

public class TransactionResponse
{
    public string Id { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string SourceId { get; set; } = string.Empty;
    public string SourceType { get; set; } = string.Empty;
    public string TransactionType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string MerchantName { get; set; } = string.Empty;
    public DateTime TransactionDate { get; set; }
    public DateTime ProcessedDate { get; set; }
    public string Status { get; set; } = string.Empty;
}
