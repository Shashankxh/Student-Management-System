using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Application.Request.StudentFeeReceipt;
using StudentManagementSystem.Application.Responce;
using StudentManagementSystem.Shared.GlobalResponce;

namespace StudentManagementSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentFeeReceiptController : BaseController
{
    [HttpPost]
    [Route("Add")]
    [Authorize(Roles = "Admin")]
    public async Task<Result<StudentFeeReceiptsResponce>> Post([FromBody] AddStudentFeeReceipt request)
    {
        var result = await Mediator.Send(request);
        return result;
    }
    [HttpPut]
    [Route("Update")]
    [Authorize(Roles = "Admin")]
    public async Task<Result<StudentFeeReceiptsResponce>> Update([FromBody] UpdateStudentFeeReceipt request)
    {
        var result = await Mediator.Send(request);
        return result;
    }
    [HttpGet]
    [Route("GetAll")]
    [Authorize(Roles = "Admin,user")]
    public async Task<Result<List<StudentFeeReceiptsResponce>>> GetAll([FromQuery] GetAllStudentFeeReceipt request)
    {
        var result = await Mediator.Send(request);
        return result;
    }
    [HttpGet]
    [Route("GetById")]
    [Authorize(Roles = "Admin,user")]
    public async Task<Result<StudentFeeReceiptsResponce>> GetById([FromQuery] GetStudentFeeReceiptById request)
    {
        var result = await Mediator.Send(request);
        return result;
    }
}
