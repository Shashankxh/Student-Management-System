using MediatR;
using StudentManagementSystem.Application.Responce;
using StudentManagementSystem.Shared.GlobalResponce;

namespace StudentManagementSystem.Application.Request.StudentDetail;

public class GetAllStudentDetails : IRequest<Result<List<StudentDetailsResponce>>>
{
    public string? FilterOn { get; set; }
    public string? FilterQuery { get; set; }
    public string? SortOn { get; set; }
    public bool IsAscending { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
}
