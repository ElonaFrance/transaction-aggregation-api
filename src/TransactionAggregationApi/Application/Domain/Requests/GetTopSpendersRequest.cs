namespace TransactionAggregationApi.Application.Domain.Requests;

public class GetTopSpendersRequest
{
    public int TopN { get; set; } = 10;
    public string? PeriodKey { get; set; }
}
