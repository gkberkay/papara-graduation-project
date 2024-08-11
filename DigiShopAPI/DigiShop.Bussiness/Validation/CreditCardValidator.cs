using DigiShop.Schema;
using FluentValidation;

public class CreditCardInfoValidator : AbstractValidator<CreditCardInfo>
{
    public CreditCardInfoValidator()
    {
        RuleFor(x => x.CardNo)
            .NotEmpty().WithMessage("Card number is required.")
            .Length(16).WithMessage("Card number must be 16 digits long.");

        RuleFor(x => x.CVV)
            .InclusiveBetween(100, 9999).WithMessage("CVV must be a 3 or 4 digit number.");

        RuleFor(x => x.ExpirationMonth)
            .InclusiveBetween(1, 12).WithMessage("Expiration month must be between 1 and 12.");

        RuleFor(x => x.ExpirationYear)
            .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("Expiration year must be the current year or later.")
            .LessThanOrEqualTo(DateTime.Now.Year + 10).WithMessage("Expiration year must be within 10 years.");
    }
}
