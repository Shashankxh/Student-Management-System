using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace StudentManagementSystem.Application.Behavior;

public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(next);

        if (_validators.Any())
        {
            ValidationContext<TRequest>? context = new(request);

            ValidationResult[]? validationResults = await Task.WhenAll(
                _validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken))).ConfigureAwait(false);

            List<ValidationFailure>? failures = [..validationResults
            .Where(r => r.Errors.Count > 0)
            .SelectMany(r => r.Errors)];

            if (failures.Count > 0)
                throw new ValidationException(failures);
        }

        return await next(cancellationToken).ConfigureAwait(false);
    }
}
