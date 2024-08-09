using DigiShop.Schema;
using FluentValidation;

namespace DigiShop.Bussiness.Validation
{
    public class UserValidator : AbstractValidator<UserRequest>
    {
        private readonly DigiShopDbContext _dbContext;
        public UserValidator(DigiShopDbContext dbContext)
        {
            _dbContext = dbContext;
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot be longer than 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot be longer than 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Please specify an email")
                .EmailAddress().WithMessage("Please specify a valid email address");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Please specify a password")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long");



        }

        //private bool BeUniqueEmail(string email)
        //{
        //    return !_dbContext.Customers.Any(c => c.Email == email);
        //}

        //private bool BeUniqueIdentityNumber(string identityNumber)
        //{
        //    return !_dbContext.Customers.Any(c => c.IdentityNumber == identityNumber);
        //}
    }
}
