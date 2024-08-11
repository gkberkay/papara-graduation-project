using DigiShop.Schema;
using FluentValidation;

public class OrderValidator : AbstractValidator<OrderRequest>
{
    public OrderValidator(DigiShopDbContext dbContext)
    {
        RuleFor(x => x.CreditCardInfo)
            .NotEmpty().WithMessage("Credit card info is required.")
            .SetValidator(new CreditCardInfoValidator());

        RuleFor(x => x.Products)
            .NotEmpty().WithMessage("Product list cannot be empty.");

        RuleFor(x => x.UserId)
            .GreaterThanOrEqualTo(0).WithMessage("User Id must be a valid non-negative number.");
    }
}
