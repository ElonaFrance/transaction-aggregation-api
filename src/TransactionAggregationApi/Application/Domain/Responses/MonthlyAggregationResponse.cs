namespace TransactionAggregationApi.Application.Domain.Responses;

public class MonthlyAggregationResponse
{
    public string ClientId { get; set; } = string.Empty;
    public IEnumerable<MonthlyAggregationItem> Monthly { get; set; } = [];
}

public class MonthlyAggregationItem
{
    public string PeriodKey { get; set; } = string.Empty;
    public decimal TotalDebit { get; set; }
    public decimal TotalCredit { get; set; }
    public int TransactionCount { get; set; }
    public decimal NetFlow => TotalCredit - TotalDebit;
}
