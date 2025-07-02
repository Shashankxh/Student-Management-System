using StudentManagementSystem.Domain.Entity;

namespace StudentManagementSystem.Application.Interface;

public interface IStudentFeeReceiptsRepository : IGenricRepository<StudentFeeReceipts>
{
    Task<string> StudentFeeReceiptsMaxId();
}
