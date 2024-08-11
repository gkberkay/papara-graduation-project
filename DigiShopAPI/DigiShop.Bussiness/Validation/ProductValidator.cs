using DigiShop.Schema;
using FluentValidation;

namespace DigiShop.Bussiness.Command
{
    public class ProductValidator : AbstractValidator<ProductRequest>
    {
        private readonly DigiShopDbContext _dbContext;

        public ProductValidator(DigiShopDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Please specify a product name");

            RuleFor(x => x.PointsPercentage)
                .NotEmpty().WithMessage("Please specify points percentage");

            RuleFor(x => x.MaxPoints)
                .NotEmpty().WithMessage("Please specify max points");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Please provide a description");
        }
    }
}
