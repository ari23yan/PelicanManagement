using FluentValidation;
using PelicanManagement.Domain.Dtos.User;
using PelicanManagement.Domain.Enums;

namespace PelicanManagement.Presentation.Validators.User
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
