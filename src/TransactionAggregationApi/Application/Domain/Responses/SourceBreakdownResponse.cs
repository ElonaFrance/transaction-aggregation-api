namespace TransactionAggregationApi.Application.Domain.Responses;

public class SourceBreakdownResponse
{
    public string ClientId { get; set; } = string.Empty;
    public IEnumerable<SourceBreakdownItem> Breakdown { get; set; } = [];
}

public class SourceBreakdownItem
{
    public string SourceType { get; set; } = string.Empty;
    public decimal TotalDebit { get; set; }
    public decimal TotalCredit { get; set; }
    public int TransactionCount { get; set; }
}
