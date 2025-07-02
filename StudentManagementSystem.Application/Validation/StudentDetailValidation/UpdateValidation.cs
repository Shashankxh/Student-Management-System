using FluentValidation;
using StudentManagementSystem.Application.Request.StudentDetail;

namespace StudentManagementSystem.Application.Validation.StudentDetailValidation;

public class UpdateValidation : AbstractValidator<UpdateStudentDetails>
{
    public UpdateValidation()
    {
        RuleFor(x => x.StudentName)
                .NotEmpty().WithMessage("Student Name is required.")
                .MinimumLength(3).WithMessage("Student Name must be at least 3 characters long.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required.").GreaterThan(1000000000).WithMessage("Phone Number must be exactly 10 digits long.")
            .LessThanOrEqualTo(9999999999).WithMessage("Phone Number must be exactly 10 digits long.");

        RuleFor(x => x.ClassName)
            .NotEmpty().WithMessage("Class Name is required.")
            .MaximumLength(50).WithMessage("Class Name must not exceed 50 characters.");

        RuleFor(x => x.FatherName)
            .NotEmpty().WithMessage("Father Name is required.")
            .MaximumLength(200).WithMessage("Father Name must not exceed 200 characters.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of Birth is required.").Must(x=>BeAValidDate(x!))
            .WithMessage("Date of Birth must be in the format DD/MM/YYYY or DD-MM-YYYY.");
    }
    private static bool BeAValidDate(string date)
    {
        if (date == null) return false;
        if ((date[2] == '-' && date[5] == '-') || (date[2] == '/' && date[5] == '/'))
        {
            return true;
        }
        return false;
    }
}
