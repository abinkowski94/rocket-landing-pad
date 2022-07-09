using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Services;

internal class AreaService : ILandingAreaService
{
    private readonly IAreaFactory _landingAreaFactory;

    public AreaService(IAreaFactory landingAreaFactory)
    {
        _landingAreaFactory = landingAreaFactory;
    }

    public Result<LandingArea> Create(string name, Size size)
    {
        return _landingAreaFactory.Create(name, size);
    }

    public Result AssignLandingPlatform(AssignPlatformRequest request)
    {
        var rectangularCoordinates = Boundary.From(request.Position!, request.Platform!.Size);

        request.Area!.AssignPlatform(request.Platform, rectangularCoordinates);

        return new();
    }

    public Result UnassignLandingPlatform(UnassignPlatformRequest request)
    {
        request.Area!.UnassignPlatform(request.Platform!);

        return new();
    }
}
