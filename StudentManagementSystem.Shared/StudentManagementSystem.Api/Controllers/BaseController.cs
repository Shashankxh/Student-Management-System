﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator Mediator => _mediator = HttpContext.RequestServices.GetService<IMediator>()!;
}
