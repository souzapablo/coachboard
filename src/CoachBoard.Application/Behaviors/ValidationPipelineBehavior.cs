using CoachBoard.Core.Exceptions;
using FluentValidation;
using MediatR;

namespace CoachBoard.Application.Behaviors;


public class ValidatorPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatorPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators;

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return next();

        var failures = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(result => result.Errors)
            .ToArray();

        if (failures.Length <= 0) return next();

        var errors = failures
            .GroupBy(x => x.PropertyName)
            .ToDictionary(k => k.Key, v => v.Select(x => x.ErrorMessage).ToArray());
        throw new InputValidationException(errors);
    }
}