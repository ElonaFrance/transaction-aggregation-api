namespace TransactionAggregationApi.Application.Domain.Responses;

public class ClientAggregationResponse
{
    public string ClientId { get; set; } = string.Empty;
    public decimal TotalDebit { get; set; }
    public decimal TotalCredit { get; set; }
    public int TransactionCount { get; set; }
    public decimal NetFlow => TotalCredit - TotalDebit;
    public DateTime LastUpdated { get; set; }
}
