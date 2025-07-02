using StudentManagementSystem.Application.Interface;
using StudentManagementSystem.Infrastuture.DataBase;

namespace StudentManagementSystem.Infrastuture.Repository;

public class UnitofWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;
    private IStudentDetailsRepository? _studentDetailRepository;
    private IStudentFeeReceiptsRepository? _studentFeeReceiptsRepository;

    public IStudentDetailsRepository StudentDetail
    {
        get
        {
            _studentDetailRepository ??= new StudentDetailsRepository(_context);
            return _studentDetailRepository;
        }
    }

    public IStudentFeeReceiptsRepository StudentFeeReceipt
    {
        get
        {
            _studentFeeReceiptsRepository = new StudentFeeReceiptsRepository(_context);
            return _studentFeeReceiptsRepository;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
