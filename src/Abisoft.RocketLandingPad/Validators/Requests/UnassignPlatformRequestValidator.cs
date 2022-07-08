using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Models.Requests;

namespace Abisoft.RocketLandingPad.Validators.Requests;

internal class UnassignPlatformRequestValidator : IValidator<UnassignPlatformRequest>
{
    public Exception? Validate(UnassignPlatformRequest? request)
    {
        if (request is null)
        {
            return Consts.Errors.CanNotBeNull(
                nameof(UnassignPlatformRequest),
                nameof(request));
        }

        if (request.Area is null)
        {
            return Consts.Errors.CanNotBeNull(
                nameof(UnassignPlatformRequest),
                nameof(UnassignPlatformRequest.Area),
                nameof(request));
        }

        if (request.Platform is null)
        {
            return Consts.Errors.CanNotBeNull(
                nameof(UnassignPlatformRequest),
                nameof(UnassignPlatformRequest.Platform),
                nameof(request));
        }

        if (request.Platform.IsUnassigned)
        {
            return Consts.Errors.PlatformAlreadyUnassigned(
                nameof(UnassignPlatformRequest),
                nameof(UnassignPlatformRequest.Platform),
                nameof(request));
        }

        if (!request.Area.Contains(request.Platform))
        {
            return Consts.Errors.DoesNotContainPlatform(
                nameof(UnassignPlatformRequest),
                nameof(UnassignPlatformRequest.Area),
                request.Platform.Id,
                nameof(request));
        }

        if (request.Platform.HasRockets)
        {
            return Consts.Errors.StillContainsRockets(
                nameof(UnassignPlatformRequest),
                nameof(UnassignPlatformRequest.Platform),
                nameof(request));
        }

        return null;
    }
}
