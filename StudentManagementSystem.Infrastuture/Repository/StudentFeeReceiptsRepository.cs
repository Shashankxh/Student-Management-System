using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Application.Interface;
using StudentManagementSystem.Domain.Entity;
using StudentManagementSystem.Infrastuture.DataBase;

namespace StudentManagementSystem.Infrastuture.Repository;

public class StudentFeeReceiptsRepository(AppDbContext context) : GenricRepository<StudentFeeReceipts>(context), IStudentFeeReceiptsRepository
{
    private readonly AppDbContext _context = context;

    public async Task<string> StudentFeeReceiptsMaxId()
    {
        var result= await _context.StudentFeeReceipts.OrderByDescending(x=>x.Id).FirstOrDefaultAsync();
        return result == null ? "": result.ReceiptNo;
    }
}
