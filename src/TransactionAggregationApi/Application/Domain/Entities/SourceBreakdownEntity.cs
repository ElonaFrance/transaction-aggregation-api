namespace TransactionAggregationApi.Application.Domain.Entities;

public class SourceBreakdownEntity
{
    public string ClientId { get; set; } = string.Empty;
    public string SourceType { get; set; } = string.Empty;
    public decimal TotalDebit { get; set; }
    public decimal TotalCredit { get; set; }
    public int TransactionCount { get; set; }
}
