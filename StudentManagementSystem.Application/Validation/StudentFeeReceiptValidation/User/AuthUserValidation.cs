using FluentValidation;
using StudentManagementSystem.Application.Request.User;

namespace StudentManagementSystem.Application.Validation.User;

public class AuthUserValidation : AbstractValidator<LoginUser>
{
    public AuthUserValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("User Name is required.")
            .EmailAddress().WithMessage("MustBe a Valid Email Address");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}
