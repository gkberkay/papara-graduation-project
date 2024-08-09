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

            RuleFor(x => x.Active)
                .NotNull().WithMessage("Please specify whether the product is active");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Please provide a description");
        }

        // private bool BeUniqueEmail(string email)
        // {
        //     return !_dbContext.Customers.Any(c => c.Email == email);
        // }

        // private bool BeUniqueIdentityNumber(string identityNumber)
        // {
        //     return !_dbContext.Customers.Any(c => c.IdentityNumber == identityNumber);
        // }
    }
}
