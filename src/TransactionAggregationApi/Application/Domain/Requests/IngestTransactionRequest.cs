namespace TransactionAggregationApi.Application.Domain.Requests;

public class IngestTransactionRequest
{
    public string ClientId { get; set; } = string.Empty;
    public string SourceType { get; set; } = string.Empty;
    public string TransactionType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "ZAR";
    public string Description { get; set; } = string.Empty;
    public string MerchantName { get; set; } = string.Empty;
    public DateTime TransactionDate { get; set; }
}
