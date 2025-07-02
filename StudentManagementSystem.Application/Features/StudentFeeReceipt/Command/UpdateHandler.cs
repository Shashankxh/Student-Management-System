using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StudentManagementSystem.Application.Interface;
using StudentManagementSystem.Application.Request.StudentFeeReceipt;
using StudentManagementSystem.Application.Responce;
using StudentManagementSystem.Domain.Entity;
using StudentManagementSystem.Shared.CommonFuntion;
using StudentManagementSystem.Shared.GlobalResponce;
using System.Globalization;

namespace StudentManagementSystem.Application.Features.StudentFeeReceipt.Command;

public class UpdateHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<User> userManager) : IRequestHandler<UpdateStudentFeeReceipt, Result<StudentFeeReceiptsResponce>>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<StudentFeeReceiptsResponce>> Handle(UpdateStudentFeeReceipt request, CancellationToken cancellationToken)
    {
        var studentFeeReceipt =await _unitOfWork.StudentFeeReceipt.GetByIdAsync(request.Id);

        if (studentFeeReceipt == null)
        {
            return Result<StudentFeeReceiptsResponce>.Failure("Student Fee Receipt not found");
        }

        studentFeeReceipt.AdmissionNo = request.AdmissionNo;
        string format = "dd MM yyyy";
        CultureInfo provider = CultureInfo.InvariantCulture;
        string date = StudentFuntion.FormatDate(request.DateOfFee!);
        studentFeeReceipt.DateOfFee = DateTime.ParseExact(date, format, provider);

        studentFeeReceipt.Fee = request.Fee;
        studentFeeReceipt.Fine = request.Fine;
        studentFeeReceipt.Month = request.Month;
        studentFeeReceipt.Status = request.Status;
        studentFeeReceipt.ModifiedBy = _userManager.GetUserId(_contextAccessor!.HttpContext.User)!;
        studentFeeReceipt.ModifiedDate = DateTime.Now;

        await _unitOfWork.StudentFeeReceipt.UpdateAsync(studentFeeReceipt);
        await _unitOfWork.SaveAsync();

        var response = _mapper.Map<StudentFeeReceiptsResponce>(studentFeeReceipt);
        return Result<StudentFeeReceiptsResponce>.Success(response);
    }
}
