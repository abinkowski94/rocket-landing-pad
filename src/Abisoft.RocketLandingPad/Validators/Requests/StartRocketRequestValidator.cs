using Abisoft.RocketLandingPad;
using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Models.Requests;

namespace Abisoft.RocketStartingPad.Validators.Requests;

internal class StartRocketRequestValidator : IValidator<StartRocketRequest>
{
    public Exception? Validate(StartRocketRequest? request)
    {
        if (request is null)
        {
            return Consts.Errors.CanNotBeNull(
                nameof(StartRocketRequest),
                nameof(request));
        }

        if (request.Area is null)
        {
            return Consts.Errors.CanNotBeNull(
                nameof(StartRocketRequest),
                nameof(StartRocketRequest.Area),
                nameof(request));
        }

        if (request.Rocket is null)
        {
            return Consts.Errors.CanNotBeNull(
                nameof(StartRocketRequest),
                nameof(StartRocketRequest.Rocket),
                nameof(request));
        }

        if (!request.Rocket.IsGrounded)
        {
            return Consts.Errors.MustBeGrounded(
                nameof(StartRocketRequest),
                nameof(StartRocketRequest.Rocket),
                nameof(request));
        }

        if (!request.Area.Contains(request.Rocket))
        {
            return Consts.Errors.DoesNotContainRocket(
                nameof(StartRocketRequest),
                nameof(StartRocketRequest.Area),
                request.Area.Id,
                request.Rocket.Id,
                nameof(request));
        }

        return null;
    }
}
