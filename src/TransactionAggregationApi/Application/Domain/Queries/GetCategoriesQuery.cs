using MediatR;
using TransactionAggregationApi.Application.Domain.Responses;

namespace TransactionAggregationApi.Application.Domain.Queries;

public record GetCategoriesQuery : IRequest<ApiResponse<IEnumerable<CategoryListResponse>>>;
