using FluentValidation;
using UsersManagement.Domain.Dtos.User;
using UsersManagement.Domain.Enums;

namespace UsersManagement.Presentation.Validators.User
{
    public class UserValidator:AbstractValidator<RegisterUserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty().Equal(x=>x.ConfirmPassword).WithMessage("Password And Confirm Password Is Not Match !");
        }
    }
}
