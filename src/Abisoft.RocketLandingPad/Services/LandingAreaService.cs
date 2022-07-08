using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Services;

internal class LandingAreaService : ILandingAreaService
{
    private readonly ICoordinatesFactory _coordinatesFactory;
    private readonly ILandingAreaFactory _landingAreaFactory;

    public LandingAreaService(
        ICoordinatesFactory coordinatesFactory,
        ILandingAreaFactory landingAreaFactory)
    {
        _coordinatesFactory = coordinatesFactory;
        _landingAreaFactory = landingAreaFactory;
    }

    public Result<LandingArea> Create(string name, Size size)
    {
        return _landingAreaFactory.Create(name, size);
    }

    public Result AssignLandingPlatform(AssignPlatformRequest request)
    {
        var rectangularCoordinates = _coordinatesFactory
            .CreateBoundary(request.Coordinates!, request.Platform!.Size);

        request.Area!.AssignPlatform(request.Platform, rectangularCoordinates);

        return new();
    }

    public Result UnassignLandingPlatform(UnassignPlatformRequest request)
    {
        request.Area!.UnassignPlatform(request.Platform!);

        return new();
    }
}
