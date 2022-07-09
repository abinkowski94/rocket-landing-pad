using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Services;

internal class PlatformService : ILandingPlatformService
{
    private readonly IPlatformFactory _landingPlatformFactory;

    public PlatformService(IPlatformFactory landingPlatformFactory)
    {
        _landingPlatformFactory = landingPlatformFactory;
    }

    public Result<LandingPlatform> Create(string name, Size size)
    {
        return _landingPlatformFactory.Create(name, size);
    }
}
