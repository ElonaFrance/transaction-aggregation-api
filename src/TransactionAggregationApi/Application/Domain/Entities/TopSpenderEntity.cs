namespace TransactionAggregationApi.Application.Domain.Entities;

public class TopSpenderEntity
{
    public string ClientId { get; set; } = string.Empty;
    public decimal TotalSpend { get; set; }
    public int TransactionCount { get; set; }
    public int Rank { get; set; }
}
