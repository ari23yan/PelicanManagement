using FluentValidation;
using PelicanManagement.Domain.Dtos.Account;
using PelicanManagement.Domain.Dtos.User;

namespace PelicanManagement.Presentation.Validators.User
{
    public class AuthenticateValidator: AbstractValidator<AuthenticateDto>
    {
        public AuthenticateValidator()
        {
            RuleFor(x => x.Input).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}
