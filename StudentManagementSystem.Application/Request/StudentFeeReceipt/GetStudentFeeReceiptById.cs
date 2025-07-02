using MediatR;
using StudentManagementSystem.Application.Responce;
using StudentManagementSystem.Shared.GlobalResponce;

namespace StudentManagementSystem.Application.Request.StudentFeeReceipt;

public class GetStudentFeeReceiptById : IRequest<Result<StudentFeeReceiptsResponce>>
{
    public int Id { get; set; }
}
