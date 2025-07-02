
using FluentValidation.Results;

namespace StudentManagementSystem.Application.Exceptions;

public class ValidatonException : ApplicationException
{
    public List<string> Errors { get; set; } = [];

    public ValidatonException(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Errors.Add(error.ErrorMessage);
        }
    }
}
