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

public class AddHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<User> userManager) : IRequestHandler<AddStudentFeeReceipt, Result<StudentFeeReceiptsResponce>>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<StudentFeeReceiptsResponce>> Handle(AddStudentFeeReceipt request, CancellationToken cancellationToken)
    {
        var studentFeeReceipt = _mapper.Map<StudentFeeReceipts>(request);

        var studentFeeReceiptMaxId = await _unitOfWork.StudentFeeReceipt.StudentFeeReceiptsMaxId();
        studentFeeReceipt.ReceiptNo = StudentFuntion.GenerateReciptId(studentFeeReceiptMaxId);

        studentFeeReceipt.CreatedDate = DateTime.Now;
        studentFeeReceipt.CreatedBy = _userManager.GetUserId(_contextAccessor!.HttpContext.User)!;

        string format = "dd MM yyyy";
        CultureInfo provider = CultureInfo.InvariantCulture; 
        string date = StudentFuntion.FormatDate(request.DateOfFee!);
        studentFeeReceipt.DateOfFee = DateTime.ParseExact(date, format, provider);

        await _unitOfWork.StudentFeeReceipt.AddAsync(studentFeeReceipt);
        await _unitOfWork.SaveAsync();

        var result = _mapper.Map<StudentFeeReceiptsResponce>(studentFeeReceipt);
        return Result<StudentFeeReceiptsResponce>.Success(result);
    }
}
