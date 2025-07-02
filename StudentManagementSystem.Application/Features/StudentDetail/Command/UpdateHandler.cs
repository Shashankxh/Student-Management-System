using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StudentManagementSystem.Application.Interface;
using StudentManagementSystem.Application.Request.StudentDetail;
using StudentManagementSystem.Application.Responce;
using StudentManagementSystem.Domain.Entity;
using StudentManagementSystem.Shared.CommonFuntion;
using StudentManagementSystem.Shared.GlobalResponce;
using System.Globalization;

namespace StudentManagementSystem.Application.Features.StudentDetail.Command;

public class UpdateHandler(IUnitOfWork unitofwork, IMapper mapper,IHttpContextAccessor contextAccessor, UserManager<User> userManager) : IRequestHandler<UpdateStudentDetails, Result<StudentDetailsResponce>>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitofwork = unitofwork;

    public async Task<Result<StudentDetailsResponce>> Handle(UpdateStudentDetails request, CancellationToken cancellationToken)
    {
        StudentDetails? student = await _unitofwork.StudentDetail.GetByIdAsync(request.Id);

        if (student == null)
        {
            return Result<StudentDetailsResponce>.Failure("Entity with this id is not present");
        }

        string format = "dd MM yyyy";
        CultureInfo provider = CultureInfo.InvariantCulture;
        string date = StudentFuntion.FormatDate(request.DateOfBirth!);
        student.DateOfBirth = DateTime.ParseExact(date, format, provider);

        student.StudentName = request.StudentName;
        student.Email = request.Email;
        student.PhoneNumber = request.PhoneNumber;
        student.ClassName = request.ClassName;
        student.FatherName = request.FatherName;
        student.ModifiedBy = _userManager.GetUserId(_contextAccessor!.HttpContext.User);
        student.ModifiedDate = DateTime.Now;

        try
        {
            await _unitofwork.StudentDetail.UpdateAsync(student);
            await _unitofwork.SaveAsync();
        }
        catch
        {
            return Result<StudentDetailsResponce>.Failure("Entity with this id is not present");
        }

        var response = _mapper.Map<StudentDetailsResponce>(student);
        return Result<StudentDetailsResponce>.Success(response);

    }
}

