using MediatR;
using Microsoft.AspNetCore.Http;
using StudentManagementSystem.Shared.GlobalResponce;

namespace StudentManagementSystem.Application.Request.StudentDetail;

public class AddStudentDetails : IRequest<Result<string>>
{
    public string StudentName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public long PhoneNumber { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
    public string FatherName { get; set; } = string.Empty;
    public  string? DateOfBirth { get; set; }
}
