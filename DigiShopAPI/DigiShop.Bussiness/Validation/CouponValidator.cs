using DigiShop.Schema;
using FluentValidation;

namespace DigiShop.Bussiness.Command
{
    public class CouponValidator : AbstractValidator<CouponRequest>
    {
        private readonly DigiShopDbContext _dbContext;
        public CouponValidator(DigiShopDbContext dbContext)
        {
            _dbContext = dbContext;
            RuleFor(x => x.SalePrice)
                .NotEmpty().WithMessage("Please specify an SalePrice");
        }
    }
}
