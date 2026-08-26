namespace TransactionAggregationApi.Application.Domain.Entities;

public class MonthlyAggregationEntity
{
    public string ClientId { get; set; } = string.Empty;
    public string Period { get; set; } = string.Empty;
    public decimal TotalDebit { get; set; }
    public decimal TotalCredit { get; set; }
    public int TransactionCount { get; set; }
    public decimal NetPosition { get; set; }
}
