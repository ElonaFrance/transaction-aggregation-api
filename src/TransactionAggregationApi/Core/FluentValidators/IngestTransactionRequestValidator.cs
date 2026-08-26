using FluentValidation;
using TransactionAggregationApi.Application.Domain.Requests;

namespace TransactionAggregationApi.Core.FluentValidators;

public class IngestTransactionRequestValidator : AbstractValidator<IngestTransactionRequest>
{
    private static readonly string[] ValidSources = ["BankAccount", "CreditCard", "Investment"];
    private static readonly string[] ValidTypes = ["Debit", "Credit"];

    public IngestTransactionRequestValidator()
    {
        RuleFor(x => x.ClientId).NotEmpty().WithMessage("ClientId is required.");
        RuleFor(x => x.SourceType).NotEmpty().Must(s => ValidSources.Contains(s))
            .WithMessage("SourceType must be one of: BankAccount, CreditCard, Investment.");
        RuleFor(x => x.TransactionType).NotEmpty().Must(t => ValidTypes.Contains(t))
            .WithMessage("TransactionType must be Debit or Credit.");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0.");
        RuleFor(x => x.Currency).NotEmpty().Length(3).WithMessage("Currency must be a 3-letter code.");
        RuleFor(x => x.TransactionDate).NotEmpty().LessThanOrEqualTo(DateTime.UtcNow.AddMinutes(5))
            .WithMessage("TransactionDate cannot be in the future.");
    }
}
