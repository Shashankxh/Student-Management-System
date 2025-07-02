using Microsoft.AspNetCore.Http;
using StudentManagementSystem.Application.Exceptions;
using StudentManagementSystem.Shared.GlobalResponce;
using System.Net;
using System.Text.Json;

namespace StudentManagementSystem.Infrastuture.Middleware;

public class GlobalExceptionHandler : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            LogException.LogExceptions(ex);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        HttpStatusCode statuscode = HttpStatusCode.InternalServerError;
        Result<string> result = Result<string>.Failure(exception.Message);
        //statuscode= HttpStatusCode.InternalServerError;
        switch (exception)
        {
            case ValidatonException validationException:
                statuscode = HttpStatusCode.BadRequest;
                result.Error = string.Join(", ", validationException.Errors);
                break;
            default:
                break;
        }

        context.Response.StatusCode = (int)statuscode;

        var jsonResponse = JsonSerializer.Serialize(result);
        return context.Response.WriteAsync(jsonResponse);
    }
}
