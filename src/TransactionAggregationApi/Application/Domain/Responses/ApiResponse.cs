namespace TransactionAggregationApi.Application.Domain.Responses;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public int? ErrorCode { get; set; }

    public static ApiResponse<T> Ok(T data, string? message = null) =>
        new() { Success = true, Data = data, Message = message };

    public static ApiResponse<T> Fail(string message, int errorCode = 400) =>
        new() { Success = false, Message = message, ErrorCode = errorCode };
}

public class PagedResponse<T> : ApiResponse<IEnumerable<T>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    public static PagedResponse<T> Ok(IEnumerable<T> data, int page, int pageSize, int totalCount) =>
        new()
        {
            Success = true,
            Data = data,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
}
