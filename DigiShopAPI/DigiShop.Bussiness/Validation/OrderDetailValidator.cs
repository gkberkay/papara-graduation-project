using DigiShop.Schema;
using FluentValidation;

namespace DigiShop.Bussiness.Command
{
    public class OrderDetailValidator : AbstractValidator<OrderDetailRequest>
    {
        private readonly DigiShopDbContext _dbContext;

        public OrderDetailValidator(DigiShopDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }
}
