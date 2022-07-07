using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Models.Requests;

namespace Abisoft.RocketLandingPad.Validators.Requests;

internal class UnassignPlatformRequestValidator : IValidator<UnassignPlatformRequest>
{
    public Exception? Validate(UnassignPlatformRequest value)
    {
        if (value is null)
        {
            return new ArgumentNullException(nameof(value), $"{nameof(UnassignPlatformRequest)} can not be null.");
        }

        return null;
    }
}
