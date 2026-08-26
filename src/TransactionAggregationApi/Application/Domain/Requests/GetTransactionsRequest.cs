namespace TransactionAggregationApi.Application.Domain.Requests;

public class GetTransactionsRequest
{
    public string? ClientId { get; set; }
    public string? SourceType { get; set; }
    public int? CategoryId { get; set; }
    public string? TransactionType { get; set; }
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public string SortBy { get; set; } = "TransactionDate";
    public string SortOrder { get; set; } = "DESC";
}
