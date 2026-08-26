namespace TransactionAggregationApi.Application.Domain.Entities;

public class CategoryBreakdownEntity
{
    public string ClientId { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public int TransactionCount { get; set; }
    public decimal Percentage { get; set; }
}
