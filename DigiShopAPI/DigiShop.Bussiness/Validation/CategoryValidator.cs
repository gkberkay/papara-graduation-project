using FluentValidation;
using DigiShop.Schema;

namespace DigiShop.Bussiness.Command
{
    public class CategoryValidator : AbstractValidator<CategoryRequest>
    {
        private readonly DigiShopDbContext _dbContext;
        public CategoryValidator(DigiShopDbContext dbContext)
        {
            _dbContext = dbContext;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name cannot be longer than 50 characters.");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Url is required.")
                .MaximumLength(50).WithMessage("Url cannot be longer than 50 characters.");

            RuleFor(x => x.Tags)
                 .NotEmpty().WithMessage("Tags is required.");
        }
    }
}
