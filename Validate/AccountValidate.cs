using BusinessObject.RequestModel;
using FluentValidation;

namespace Validate
{
    public class AccountValidate :AbstractValidator<AccountRequest>
    {
        public AccountValidate() 
        {
            RuleFor(u => u.Email)
                .EmailAddress().WithMessage("Not a valid email")
                .NotEmpty().WithMessage("Email is required");
            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
