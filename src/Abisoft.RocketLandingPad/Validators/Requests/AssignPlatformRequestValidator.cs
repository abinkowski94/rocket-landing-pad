using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Requests;

namespace Abisoft.RocketLandingPad.Validators.Requests;

internal class AssignPlatformRequestValidator : IValidator<AssignPlatformRequest>
{
    private readonly IValidator<Coordinates> _coordinatesValidator;

    public AssignPlatformRequestValidator(IValidator<Coordinates> coordinatesValidator)
    {
        _coordinatesValidator = coordinatesValidator;
    }

    public Exception? Validate(AssignPlatformRequest value)
    {
        if (value is null)
        {
            return new ArgumentNullException(
                nameof(value),
                $"{nameof(AssignPlatformRequest)} can not be null.");
        }

        if (value.LandingArea is null)
        {
            return new ArgumentException(
                $"{nameof(AssignPlatformRequest)}.{nameof(AssignPlatformRequest.LandingArea)} can not be null.",
                nameof(value));
        }

        if (value.Platform is null)
        {
            return new ArgumentException(
                $"{nameof(AssignPlatformRequest)}.{nameof(AssignPlatformRequest.Platform)} can not be null.",
                nameof(value));
        }

        if (value.Coordinates is null)
        {
            return new ArgumentException(
                $"{nameof(AssignPlatformRequest)}.{nameof(AssignPlatformRequest.Coordinates)} can not be null.",
                nameof(value));
        }

        var coordinatesValidationResult = _coordinatesValidator.Validate(value.Coordinates);
        if (coordinatesValidationResult is not null)
        {
            return coordinatesValidationResult;
        }

        if (value.LandingArea.Contains(value.Platform))
        {
            return new ArgumentException(
                $"{nameof(AssignPlatformRequest)}.{nameof(AssignPlatformRequest.LandingArea)} already contains platform: '{value.Platform.Id}'.",
                nameof(value));
        }

        return null;
    }
}
