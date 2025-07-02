using AutoMapper;
using MediatR;
using StudentManagementSystem.Application.Interface;
using StudentManagementSystem.Application.Request.StudentFeeReceipt;
using StudentManagementSystem.Application.Responce;
using StudentManagementSystem.Shared.GlobalResponce;

namespace StudentManagementSystem.Application.Features.StudentFeeReceipt.Query;

public class GetAllHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllStudentFeeReceipt, Result<List<StudentFeeReceiptsResponce>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<StudentFeeReceiptsResponce>>> Handle(GetAllStudentFeeReceipt request, CancellationToken cancellationToken)
    {
        var studentFeeReceipts = await _unitOfWork.StudentFeeReceipt.GetAllAsync();
        string? filterOn = request.FilterOn;
        string? filterQuery = request.FilterQuery;
        string? sortOn = request.SortOn;
        bool isAscending = request.IsAscending;
        int pageNumber = request.PageNumber;
        int pageSize = request.PageSize;
        if (studentFeeReceipts != null)
        {
            
            if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
            {
                if (filterOn.Equals("AdmissionNo", StringComparison.OrdinalIgnoreCase))
                {
                    studentFeeReceipts = studentFeeReceipts.Where(x => x.AdmissionNo.ToString().Equals(filterQuery));
                }
                if (filterOn.Equals("ReceiptNo", StringComparison.OrdinalIgnoreCase))
                {
                    studentFeeReceipts = studentFeeReceipts.Where(x => x.ReceiptNo.ToString().Equals(filterQuery));
                }
                if (filterOn.Equals("DateOfFee", StringComparison.OrdinalIgnoreCase))
                {
                    studentFeeReceipts = studentFeeReceipts.Where(x => x.DateOfFee.ToString().Equals(filterQuery));
                }
                if (filterOn.Equals("Month", StringComparison.OrdinalIgnoreCase))
                {
                    studentFeeReceipts = studentFeeReceipts.Where(x => x.Month.ToString().Equals(filterQuery));
                }
                if (filterOn.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    studentFeeReceipts = studentFeeReceipts.Where(x => x.Status.ToString().Equals(filterQuery));
                }
            }
            
            if (!string.IsNullOrEmpty(sortOn))
            {

                if (sortOn.Equals("AdmissionNo", StringComparison.OrdinalIgnoreCase))
                {
                    studentFeeReceipts = (isAscending) ? studentFeeReceipts.OrderBy(x => x.AdmissionNo) : studentFeeReceipts.OrderByDescending(x => x.AdmissionNo);
                }
                if (sortOn.Equals("ReceiptNo", StringComparison.OrdinalIgnoreCase))
                {
                    studentFeeReceipts = (isAscending) ? studentFeeReceipts.OrderBy(x => x.ReceiptNo) : studentFeeReceipts.OrderByDescending(x => x.ReceiptNo);
                }
                if (sortOn.Equals("DateOfFee", StringComparison.OrdinalIgnoreCase))
                {
                    studentFeeReceipts = (isAscending) ? studentFeeReceipts.OrderBy(x => x.DateOfFee) : studentFeeReceipts.OrderByDescending(x => x.DateOfFee);
                }
                if (sortOn.Equals("Fee", StringComparison.OrdinalIgnoreCase))
                {
                    studentFeeReceipts = (isAscending) ? studentFeeReceipts.OrderBy(x => x.Fee) : studentFeeReceipts.OrderByDescending(x => x.Fee);
                }
                if (sortOn.Equals("Fine", StringComparison.OrdinalIgnoreCase))
                {
                    studentFeeReceipts = (isAscending) ? studentFeeReceipts.OrderBy(x => x.Fine) : studentFeeReceipts.OrderByDescending(x => x.Fine);
                }
                if (sortOn.Equals("Month", StringComparison.OrdinalIgnoreCase))
                {
                    studentFeeReceipts = (isAscending) ? studentFeeReceipts.OrderBy(x => x.Month) : studentFeeReceipts.OrderByDescending(x => x.Month);
                }
            }
            
            var paginatedResult = studentFeeReceipts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            if (paginatedResult == null || paginatedResult.Count == 0)
            {
                return Result<List<StudentFeeReceiptsResponce>>.Failure("No Student Found");
            }
            var k = _mapper.Map<List<StudentFeeReceiptsResponce>>(paginatedResult);
            return Result<List<StudentFeeReceiptsResponce>>.Success(k);
        }
        return Result<List<StudentFeeReceiptsResponce>>.Failure("No student found ");
    }
}