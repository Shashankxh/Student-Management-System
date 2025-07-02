using AutoMapper;
using MediatR;
using StudentManagementSystem.Application.Interface;
using StudentManagementSystem.Application.Request.StudentFeeReceipt;
using StudentManagementSystem.Application.Responce;
using StudentManagementSystem.Shared.GlobalResponce;

namespace StudentManagementSystem.Application.Features.StudentFeeReceipt.Query;

public class GetByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetStudentFeeReceiptById, Result<StudentFeeReceiptsResponce>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<StudentFeeReceiptsResponce>> Handle(GetStudentFeeReceiptById request, CancellationToken cancellationToken)
    {
        var studentFeeReceipt =await _unitOfWork.StudentFeeReceipt.GetByIdAsync(request.Id);

        if (studentFeeReceipt == null)
        {
            return Result<StudentFeeReceiptsResponce>.Failure("Student Fee Receipt not found");
        }

        var studentFeeReceiptResponce = _mapper.Map<StudentFeeReceiptsResponce>(studentFeeReceipt);
        return Result<StudentFeeReceiptsResponce>.Success(studentFeeReceiptResponce);
    }
}
