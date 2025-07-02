using MediatR;
using StudentManagementSystem.Application.Responce;
using StudentManagementSystem.Shared.Enums;
using StudentManagementSystem.Shared.GlobalResponce;

namespace StudentManagementSystem.Application.Request.StudentFeeReceipt;

public class UpdateStudentFeeReceipt : IRequest<Result<StudentFeeReceiptsResponce>>
{
    public int Id { get; set; }
    public string AdmissionNo { get; set; } = string.Empty;
    public string DateOfFee { get; set; } = string.Empty;
    public double Fee { get; set; }
    public double Fine { get; set; }
    public string Month { get; set; } = string.Empty;
    public FeeStatus Status { get; set; } = FeeStatus.Inprogress;
}
