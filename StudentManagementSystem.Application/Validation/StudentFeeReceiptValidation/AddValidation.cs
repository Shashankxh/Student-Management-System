using FluentValidation;
using StudentManagementSystem.Application.Interface;
using StudentManagementSystem.Application.Request.StudentFeeReceipt;

namespace StudentManagementSystem.Application.Validation.StudentFeeReceiptValidation;

public class AddValidation : AbstractValidator<AddStudentFeeReceipt>
{
    private readonly IUnitOfWork _unitOfWork;
    public AddValidation(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        RuleFor(x => x.AdmissionNo)
            .NotEmpty().WithMessage("Admission Number is required.")
            .Must(x => BeAValidAdmissionNo(x)).WithMessage("Addmission Number not in The Database");

        RuleFor(x => x.DateOfFee)
            .NotEmpty().WithMessage("Date of Fee is required.")
            .Must(date => BeAValidDate(date))
            .WithMessage("Date of Fee must be in the format DD/MM/YYYY or DD-MM-YYYY.");

        RuleFor(x => x.Fee)
            .NotEmpty().WithMessage("Fee is required.")
            .GreaterThanOrEqualTo(0).WithMessage("Fee must be greater than 0.");

        RuleFor(x => x.Fine)
            .GreaterThanOrEqualTo(0).WithMessage("Fine must be greater than or equal to 0.");

        RuleFor(x => x.Month)
            .NotEmpty().WithMessage("Month is required.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status value.");
    }
    private bool BeAValidAdmissionNo(string admissionNo)
    {
        var student = _unitOfWork.StudentDetail.GetAllAsync().Result.FirstOrDefault(x => x.AdmissionNo == admissionNo);
        return student != null;
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
