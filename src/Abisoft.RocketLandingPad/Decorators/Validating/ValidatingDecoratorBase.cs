using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Decorators.Validating;

internal abstract class ValidatingDecoratorBase
{
    protected static Result ValidateAndExecuteRequest<TRequest>(
        TRequest request,
        IValidator<TRequest> validator,
        Func<TRequest, Result> action)
    {
        var validationResult = validator.Validate(request);
        if (validationResult is not null)
        {
            return validationResult;
        }

        return action(request);
    }
}
