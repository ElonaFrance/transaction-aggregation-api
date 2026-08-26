using MediatR;
using TransactionAggregationApi.Application.Domain.Queries;
using TransactionAggregationApi.Application.Domain.Responses;

namespace TransactionAggregationApi.Application.Handlers.Queries;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, ApiResponse<IEnumerable<CategoryListResponse>>>
{
    public Task<ApiResponse<IEnumerable<CategoryListResponse>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var list = Domain.Constants.AppConstants.Categories.All
            .Select(kvp => new CategoryListResponse { Id = kvp.Key, Name = kvp.Value });
        return Task.FromResult(ApiResponse<IEnumerable<CategoryListResponse>>.Ok(list));
    }
}
