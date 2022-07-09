using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Requests;

namespace Abisoft.RocketLandingPad.Validators.Requests;

internal class LandRocketRequestValidator : IValidator<LandRocketRequest>
{
    private readonly IValidator<Coordinates> _coordinatesValidator;
    private readonly IOutlineFactory _outlineFactory;

    public LandRocketRequestValidator(
        IValidator<Coordinates> coordinatesValidator,
        IOutlineFactory outlineFactory)
    {
        _coordinatesValidator = coordinatesValidator;
        _outlineFactory = outlineFactory;
    }

    public Exception? Validate(LandRocketRequest? request)
    {
        if (request is null)
        {
            return Consts.Errors.CanNotBeNull(
                nameof(LandRocketRequest),
                nameof(request));
        }

        if (request.Area is null)
        {
            return Consts.Errors.CanNotBeNull(
                nameof(LandRocketRequest),
                nameof(LandRocketRequest.Area),
                nameof(request));
        }

        if (request.Rocket is null)
        {
            return Consts.Errors.CanNotBeNull(
                nameof(LandRocketRequest),
                nameof(LandRocketRequest.Rocket),
                nameof(request));
        }

        if (request.Position is null)
        {
            return Consts.Errors.CanNotBeNull(
                nameof(LandRocketRequest),
                nameof(LandRocketRequest.Position),
                nameof(request));
        }

        var coordinatesValidationResult = _coordinatesValidator.Validate(request.Position);
        if (coordinatesValidationResult is not null)
        {
            return coordinatesValidationResult;
        }

        request.Platform = TryFindPlatform(request.Area, request.Position);

        if (request.Platform is null)
        {
            return Consts.Errors.OutOfPlatform(
                nameof(LandRocketRequest),
                nameof(LandRocketRequest.Position),
                nameof(request));
        }

        request.Outline = _outlineFactory.Create(request.Platform.AssignedArea!.Boundary, request.Position);

        if (request.Area.OccupiedCoordinates.Contains(request.Position))
        {
            return Consts.Errors.Clashes(
                nameof(LandRocketRequest),
                nameof(LandRocketRequest.Position),
                nameof(request));
        }

        foreach (var rocket in request.Area.Rockets)
        {
            if (request.Outline.Any(o => o == rocket.OccupiedPlatform!.Position))
            {
                return Consts.Errors.Clashes(
                    nameof(LandRocketRequest),
                    nameof(LandRocketRequest.Position),
                    nameof(request));
            }
        }

        return null;
    }

    private static LandingPlatform? TryFindPlatform(LandingArea area, Coordinates position)
    {
        return area.Platforms.FirstOrDefault(p => p.AssignedArea!.Boundary.Contains(position));
    }
}
