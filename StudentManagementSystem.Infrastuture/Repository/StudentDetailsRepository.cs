using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Application.Interface;
using StudentManagementSystem.Domain.Entity;
using StudentManagementSystem.Infrastuture.DataBase;

namespace StudentManagementSystem.Infrastuture.Repository;

public class StudentDetailsRepository(AppDbContext context) : GenricRepository<StudentDetails>(context), IStudentDetailsRepository
{
    private readonly AppDbContext _context = context;
    public async Task<string> GetStudentDetailsMaxIdAsync()
    {
        StudentDetails? result= await _context.StudentDetails.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
        return result==null ? "": result.AdmissionNo;
    }
}
