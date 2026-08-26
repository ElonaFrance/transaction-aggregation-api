namespace TransactionAggregationApi.Application.Domain.Entities;

public class ClientAggregationEntity
{
    public string ClientId { get; set; } = string.Empty;
    public decimal TotalDebit { get; set; }
    public decimal TotalCredit { get; set; }
    public int TransactionCount { get; set; }
    public decimal NetPosition { get; set; }
    public DateTime LastUpdated { get; set; }
}
