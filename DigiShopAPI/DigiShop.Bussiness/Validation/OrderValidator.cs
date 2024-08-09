using DigiShop.Base.Schema;
using DigiShop.Schema;
using FluentValidation;

namespace DigiShop.Bussiness.Command
{
    public class OrderValidator : AbstractValidator<OrderRequest>
    {
        private readonly DigiShopDbContext _dbContext;

        public OrderValidator(DigiShopDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.OrderNumber)
                .NotEmpty().WithMessage("Order number is required.")
                .MaximumLength(50).WithMessage("Order number cannot be longer than 50 characters.");

            RuleFor(x => x.PointsUsed)
                .NotNull().WithMessage("Points used is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Points used must be a valid non-negative number.");

            RuleFor(x => x.CouponAmount)
                .NotNull().WithMessage("Coupon amount is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Coupon amount must be a valid non-negative number.");

            RuleFor(x => x.CouponCode)
                .NotEmpty().WithMessage("Coupon code is required.")
                .MaximumLength(50).WithMessage("Coupon code cannot be longer than 50 characters.");
        }
    }
}
