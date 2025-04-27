using eCommerce.Core.DTO;
using FluentValidation;

namespace eCommerce.Core.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator() 
        {
            //Email Validation
            RuleFor(emailVal => emailVal.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Invalid email address format");
            //Password Validation
            RuleFor(passwordVal => passwordVal.Password)
                .NotEmpty()
                .WithMessage("Password is required");
        }
    }
}
