using MediatR;
using StudentManagementSystem.Application.Responce;
using StudentManagementSystem.Shared.GlobalResponce;

namespace StudentManagementSystem.Application.Request.StudentDetail;

public class UpdateStudentDetails : IRequest<Result<StudentDetailsResponce>>
{
    public int Id { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public long PhoneNumber { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    public string? DateOfBirth { get; set; }

}
