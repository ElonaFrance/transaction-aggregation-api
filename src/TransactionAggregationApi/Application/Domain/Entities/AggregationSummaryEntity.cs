namespace TransactionAggregationApi.Application.Domain.Entities;

public class AggregationSummaryEntity
{
    public decimal TotalDebit { get; set; }
    public decimal TotalCredit { get; set; }
    public int TotalTransactionCount { get; set; }
    public int UniqueClientCount { get; set; }
    public string TopCategory { get; set; } = string.Empty;
    public string TopSource { get; set; } = string.Empty;
    public decimal AverageTransactionAmount { get; set; }
}
