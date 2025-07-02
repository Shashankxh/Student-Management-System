using MediatR;
using StudentManagementSystem.Application.Responce;
using StudentManagementSystem.Shared.GlobalResponce;

namespace StudentManagementSystem.Application.Request.StudentFeeReceipt;

public class GetAllStudentFeeReceipt : IRequest<Result<List<StudentFeeReceiptsResponce>>>
{
    public string? FilterOn { get; set; }
    public string? FilterQuery { get; set; }
    public string? SortOn { get; set; }
    public bool IsAscending { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
}
