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

        if (request.Position is null)
        {
            return Consts.Errors.CanNotBeNull(
                nameof(AssignPlatformRequest),
                nameof(AssignPlatformRequest.Position),
                nameof(request));
        }

        var coordinatesValidationResult = _coordinatesValidator.Validate(request.Position);
        if (coordinatesValidationResult is not null)
        {
            return coordinatesValidationResult;
        }

        if (request.Area.Contains(request.Platform))
        {
            return Consts.Errors.AlreadyContainsPlatform(
                nameof(AssignPlatformRequest),
                nameof(AssignPlatformRequest.Area),
                request.Platform.Id,
                nameof(request));
        }

        if (!request.Area.Boundary.Contains(request.Position, request.Platform.Size))
        {
            return Consts.Errors.DoesNotFitInsideArea(
                nameof(AssignPlatformRequest),
                nameof(AssignPlatformRequest.Platform),
                request.Platform.Id,
                request.Area.Id,
                nameof(request));
        }

        foreach (var existingPlatform in request.Area.Platforms)
        {
            if (existingPlatform.AssignedArea!.Boundary
                .Overlaps(request.Position, request.Platform.Size))
            {
                return Consts.Errors.OverlapsWithExistingPlatform(
                    nameof(AssignPlatformRequest),
                    nameof(AssignPlatformRequest.Platform),
                    request.Platform.Id,
                    existingPlatform.Id,
                    nameof(request));
            }
        }

        return null;
    }
}
