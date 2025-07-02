using MediatR;
using StudentManagementSystem.Application.Interface;
using StudentManagementSystem.Application.Request.StudentDetail;
using StudentManagementSystem.Shared.GlobalResponce;

namespace StudentManagementSystem.Application.Features.StudentDetail.Command;

public class DeleteHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteStudentDetails, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<string>> Handle(DeleteStudentDetails request, CancellationToken cancellationToken)
    {
        var studentDetail = await _unitOfWork.StudentDetail.GetByIdAsync(request.Id);

        if (studentDetail == null)
        {
            return Result<string>.Failure("Student detail not found.");
        }

        await _unitOfWork.StudentDetail.DeleteAsync(studentDetail);
        await _unitOfWork.SaveAsync();

        return Result<string>.Success("Student detail deleted successfully.");
    }
}
