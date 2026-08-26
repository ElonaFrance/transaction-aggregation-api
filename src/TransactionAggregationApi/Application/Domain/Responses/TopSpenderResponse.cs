namespace TransactionAggregationApi.Application.Domain.Responses;

public class TopSpenderResponse
{
    public string ClientId { get; set; } = string.Empty;
    public decimal TotalDebit { get; set; }
    public int TransactionCount { get; set; }
    public int Rank { get; set; }
}
