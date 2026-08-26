using FluentValidation;
using TransactionAggregationApi.Application.Domain.Requests;

namespace TransactionAggregationApi.Core.FluentValidators;

public class GetTransactionsRequestValidator : AbstractValidator<GetTransactionsRequest>
{
    public GetTransactionsRequestValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0).WithMessage("Page must be greater than 0.");
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100.");
        RuleFor(x => x.MinAmount).GreaterThanOrEqualTo(0).When(x => x.MinAmount.HasValue);
        RuleFor(x => x.MaxAmount).GreaterThan(0).When(x => x.MaxAmount.HasValue);
        RuleFor(x => x).Must(x => !x.MinAmount.HasValue || !x.MaxAmount.HasValue || x.MaxAmount >= x.MinAmount)
            .WithMessage("MaxAmount must be greater than or equal to MinAmount.");
        RuleFor(x => x).Must(x => !x.FromDate.HasValue || !x.ToDate.HasValue || x.ToDate >= x.FromDate)
            .WithMessage("ToDate must be after FromDate.");
    }
}
