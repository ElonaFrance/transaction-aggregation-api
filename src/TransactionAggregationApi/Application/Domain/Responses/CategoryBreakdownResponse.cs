namespace TransactionAggregationApi.Application.Domain.Responses;

public class CategoryBreakdownResponse
{
    public string ClientId { get; set; } = string.Empty;
    public IEnumerable<CategoryBreakdownItem> Breakdown { get; set; } = [];
}

public class CategoryBreakdownItem
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public int TransactionCount { get; set; }
}
