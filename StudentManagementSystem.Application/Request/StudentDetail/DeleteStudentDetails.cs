using MediatR;
using StudentManagementSystem.Shared.GlobalResponce;

namespace StudentManagementSystem.Application.Request.StudentDetail;

public class DeleteStudentDetails : IRequest<Result<string>>
{
    public int Id { get; set; }
}
