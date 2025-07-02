using MediatR;
using StudentManagementSystem.Application.Responce;
using StudentManagementSystem.Shared.GlobalResponce;

namespace StudentManagementSystem.Application.Request.StudentDetail;

public class GetStudentDetailsById : IRequest<Result<StudentDetailsResponce>>
{
    public int Id { get; set; }
}
