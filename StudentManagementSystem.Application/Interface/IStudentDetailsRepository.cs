using StudentManagementSystem.Domain.Entity;

namespace StudentManagementSystem.Application.Interface;

public interface IStudentDetailsRepository : IGenricRepository<StudentDetails>
{
    Task<string> GetStudentDetailsMaxIdAsync();
}
