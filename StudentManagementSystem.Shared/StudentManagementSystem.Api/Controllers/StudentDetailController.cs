using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Application.Request.StudentDetail;
using StudentManagementSystem.Application.Responce;
using StudentManagementSystem.Shared.GlobalResponce;

namespace StudentManagementSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentDetailController : BaseController
{
    [HttpPost]
    [Route("Add")]
    [Authorize(Roles = "Admin")]
    public async Task<Result<string>> Post([FromQuery] AddStudentDetails studentDetails)
    {
        var result = await Mediator.Send(studentDetails);
        return result;
    }

    [HttpPut]
    [Route("Update")]
    [Authorize(Roles = "Admin")]
    public async Task<Result<StudentDetailsResponce>> Update([FromBody] UpdateStudentDetails studentDetails)
    {
        var result = await Mediator.Send(studentDetails);
        return result;
    }
    [HttpDelete]
    [Route("Delete")]
    [Authorize(Roles = "Admin")]
    public async Task<Result<string>> Delete([FromQuery] int id)
    {
        var result = await Mediator.Send(new DeleteStudentDetails { Id = id });
        return result;
    }

    [HttpGet]
    [Route("Get")]
    [Authorize(Roles = "Admin,user")]
    public async Task<Result<StudentDetailsResponce>> GetById([FromQuery] int id)
    {
        var result = await Mediator.Send(new GetStudentDetailsById { Id = id });
        return result;
    }

    [HttpGet("GetAll")]
    [Authorize(Roles = "Admin,user")]
    public async Task<Result<List<StudentDetailsResponce>>> GetAll([FromQuery] GetAllStudentDetails details)
    {
        var result = await Mediator.Send(details);
        return result;
    }
}
