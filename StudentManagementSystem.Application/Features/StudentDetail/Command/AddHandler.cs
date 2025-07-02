using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StudentManagementSystem.Application.Interface;
using StudentManagementSystem.Application.Request.StudentDetail;
using StudentManagementSystem.Domain.Entity;
using StudentManagementSystem.Shared.CommonFuntion;
using StudentManagementSystem.Shared.GlobalResponce;
using System.Globalization;

namespace StudentManagementSystem.Application.Features.StudentDetail.Command;

public class AddHandler(IUnitOfWork unitofwork, IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<User> userManager) : IRequestHandler<AddStudentDetails, Result<string>>
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitofwork = unitofwork;
    public async Task<Result<string>> Handle(AddStudentDetails request, CancellationToken cancellationToken)
    {
        var student = _mapper.Map<StudentDetails>(request);

        var admissionno = _unitofwork.StudentDetail.GetStudentDetailsMaxIdAsync().Result;

        student.AdmissionNo = StudentFuntion.GenerateAdmitionId(admissionno);
        string format = "dd MM yyyy";
        CultureInfo provider = CultureInfo.InvariantCulture;
        string date = StudentFuntion.FormatDate(request.DateOfBirth!);
        student.DateOfBirth = DateTime.ParseExact(date, format, provider);
        student.CreatedDate = DateTime.Now;

        string imageName = request.Image!.FileName;
        imageName = imageName.Replace(".jpg", "");
        string imageExtention = Path.GetExtension(request.Image.FileName);

        var imagesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
        if (!Directory.Exists(imagesDirectory))
        {
            Directory.CreateDirectory(imagesDirectory);
        }
        var localPath = Path.Combine(imagesDirectory, $"{imageName}{imageExtention}");
        using var stream = new FileStream(localPath, FileMode.Create);
        await request.Image.CopyToAsync(stream, cancellationToken);

        var urlFilePath = $"{_contextAccessor?.HttpContext?.Request.Scheme}://{_contextAccessor?.HttpContext?.Request.Host}{_contextAccessor?.HttpContext?.Request.PathBase}/Images/{imageName}{imageExtention}";
        student.StudentImage = urlFilePath;
        //student.CreatedBy = _contextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value;
        student.CreatedBy = _userManager.GetUserId(_contextAccessor!.HttpContext.User);

        await _unitofwork.StudentDetail.AddAsync(student);
        await _unitofwork.SaveAsync();
        return Result<string>.Success($"Student Added AdmissionNo : {student.AdmissionNo}");
    }
}
