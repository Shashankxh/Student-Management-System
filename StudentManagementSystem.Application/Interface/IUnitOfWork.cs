namespace StudentManagementSystem.Application.Interface;

public interface IUnitOfWork
{
    IStudentDetailsRepository StudentDetail { get; }
    IStudentFeeReceiptsRepository StudentFeeReceipt { get; }
    Task SaveAsync();
}
