namespace TransactionAggregationApi.Application.Domain.Responses;

public class AggregationSummaryResponse
{
    public decimal TotalDebit { get; set; }
    public decimal TotalCredit { get; set; }
    public int TotalTransactions { get; set; }
    public int UniqueClients { get; set; }
    public string TopCategory { get; set; } = string.Empty;
    public string TopSource { get; set; } = string.Empty;
    public decimal NetFlow => TotalCredit - TotalDebit;
}
