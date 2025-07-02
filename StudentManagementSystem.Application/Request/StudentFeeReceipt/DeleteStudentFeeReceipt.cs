using MediatR;
using StudentManagementSystem.Shared.GlobalResponce;

namespace StudentManagementSystem.Application.Request.StudentFeeReceipt;

public class DeleteStudentFeeReceipt : IRequest<Result<String>>
{
    public int Id { get; set; }
}
