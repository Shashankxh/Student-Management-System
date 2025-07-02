using MediatR;
using StudentManagementSystem.Application.Interface;
using StudentManagementSystem.Application.Request.StudentFeeReceipt;
using StudentManagementSystem.Shared.GlobalResponce;

namespace StudentManagementSystem.Application.Features.StudentFeeReceipt.Command;
public class DeleteHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteStudentFeeReceipt, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<string>> Handle(DeleteStudentFeeReceipt request, CancellationToken cancellationToken)
    {
        var studentFeeReceipt = await _unitOfWork.StudentFeeReceipt.GetByIdAsync(request.Id);

        if (studentFeeReceipt == null)
        {
            return Result<string>.Failure("Student Fee Receipt not found.");
        }

        await _unitOfWork.StudentFeeReceipt.DeleteAsync(studentFeeReceipt);
        await _unitOfWork.SaveAsync();

        return Result<string>.Success("Student Fee Receipt deleted successfully.");
    }
}
