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

    public Exception? Validate(AssignPlatformRequest? request)
    {
        if (request is null)
        {
            return Consts.Errors.CanNotBeNull(
                nameof(AssignPlatformRequest),
                nameof(request));
        }

        if (request.Area is null)
        {
            return Consts.Errors.CanNotBeNull(
                nameof(AssignPlatformRequest),
                nameof(AssignPlatformRequest.Area),
                nameof(request));
        }

        if (request.Platform is null)
        {
            return Consts.Errors.CanNotBeNull(
                nameof(AssignPlatformRequest),
                nameof(AssignPlatformRequest.Platform),
                nameof(request));
        }

        var coordinatesValidationResult = _coordinatesValidator.Validate(request.Coordinates);
        if (coordinatesValidationResult is not null)
        {
            return coordinatesValidationResult;
        }

        if (request.Area.Contains(request.Platform))
        {
            return Consts.Errors.AlreadyContainsPlatform(
                nameof(AssignPlatformRequest),
                nameof(AssignPlatformRequest.Platform),
                request.Platform.Id,
                nameof(request));
        }

        return null;
    }
}
