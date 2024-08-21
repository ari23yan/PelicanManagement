using FluentValidation;
using UsersManagement.Domain.Dtos.Account;
using UsersManagement.Domain.Dtos.User;

namespace UsersManagement.Presentation.Validators.User
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
