using AutoMapper;
using MediatR;
using StudentManagementSystem.Application.Interface;
using StudentManagementSystem.Application.Request.StudentDetail;
using StudentManagementSystem.Application.Responce;
using StudentManagementSystem.Shared.GlobalResponce;

namespace StudentManagementSystem.Application.Features.StudentDetail.Query;

public class GetAllHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllStudentDetails, Result<List<StudentDetailsResponce>>>
{
    private readonly IUnitOfWork _unitOfwork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<StudentDetailsResponce>>> Handle(GetAllStudentDetails request, CancellationToken cancellationToken)
    {
        var studentDetails = await _unitOfwork.StudentDetail.GetAllAsync();

        string? filterOn = request.FilterOn;
        string? filterQuery = request.FilterQuery;
        string? sortOn = request.SortOn;
        bool isAscending = request.IsAscending;
        int pageNumber = request.PageNumber;
        int pageSize = request.PageSize;

        if (studentDetails != null)
        {
            
            if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
            {
                if (filterOn.Equals("AdmissionNo", StringComparison.OrdinalIgnoreCase))
                {
                    studentDetails = studentDetails.Where(x => x.AdmissionNo.ToString().Equals(filterQuery));
                }
                if (filterOn.Equals("StudentName", StringComparison.OrdinalIgnoreCase))
                {
                    studentDetails = studentDetails.Where(x => x.StudentName.ToString().Equals(filterQuery));
                }
                if (filterOn.Equals("Email", StringComparison.OrdinalIgnoreCase))
                {
                    studentDetails = studentDetails.Where(x => x.Email.ToString().Equals(filterQuery));
                }
                if (filterOn.Equals("PhoneNumber", StringComparison.OrdinalIgnoreCase))
                {
                    studentDetails = studentDetails.Where(x => x.PhoneNumber.ToString().Equals(filterQuery));
                }
                if (filterOn.Equals("ClassName", StringComparison.OrdinalIgnoreCase))
                {
                    studentDetails = studentDetails.Where(x => x.ClassName.ToString().Equals(filterQuery));
                }
                if (filterOn.Equals("FatherName", StringComparison.OrdinalIgnoreCase))
                {
                    studentDetails = studentDetails.Where(x => x.FatherName.ToString().Equals(filterQuery));
                }
                if (filterOn.Equals("DateOfBirth", StringComparison.OrdinalIgnoreCase))
                {
                    studentDetails = studentDetails.Where(x => x.DateOfBirth.ToString().Equals(filterQuery));
                }

            }
            
            if (!string.IsNullOrEmpty(sortOn))
            {

                if (sortOn.Equals("AdmissionNo", StringComparison.OrdinalIgnoreCase))
                {
                    studentDetails = (isAscending) ? studentDetails.OrderBy(x => x.AdmissionNo) : studentDetails.OrderByDescending(x => x.AdmissionNo);
                }
                if (sortOn.Equals("StudentName", StringComparison.OrdinalIgnoreCase))
                {
                    studentDetails = (isAscending) ? studentDetails.OrderBy(x => x.StudentName) : studentDetails.OrderByDescending(x => x.StudentName);
                }
                if (sortOn.Equals("Email", StringComparison.OrdinalIgnoreCase))
                {
                    studentDetails = (isAscending) ? studentDetails.OrderBy(x => x.Email) : studentDetails.OrderByDescending(x => x.Email);
                }
                if (sortOn.Equals("PhoneNumber", StringComparison.OrdinalIgnoreCase))
                {
                    studentDetails = (isAscending) ? studentDetails.OrderBy(x => x.PhoneNumber) : studentDetails.OrderByDescending(x => x.PhoneNumber);
                }
                if (sortOn.Equals("ClassName", StringComparison.OrdinalIgnoreCase))
                {
                    studentDetails = (isAscending) ? studentDetails.OrderBy(x => x.ClassName) : studentDetails.OrderByDescending(x => x.ClassName);
                }
                if (sortOn.Equals("FatherName", StringComparison.OrdinalIgnoreCase))
                {
                    studentDetails = (isAscending) ? studentDetails.OrderBy(x => x.FatherName) : studentDetails.OrderByDescending(x => x.FatherName);
                }
                if (sortOn.Equals("DateOfBirth", StringComparison.OrdinalIgnoreCase))
                {
                    studentDetails = (isAscending) ? studentDetails.OrderBy(x => x.DateOfBirth) : studentDetails.OrderByDescending(x => x.DateOfBirth);
                }
            }
            
            var paginatedResult = studentDetails.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            if (paginatedResult == null || paginatedResult.Count == 0)
            {
                return Result<List<StudentDetailsResponce>>.Failure("No Student Found");
            }

            var k = _mapper.Map<List<StudentDetailsResponce>>(paginatedResult);
            return Result<List<StudentDetailsResponce>>.Success(k);
        }
        return Result<List<StudentDetailsResponce>>.Failure("No student found ");
    }
}
