using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Abstractions.Services;

public interface ILandingPlatformService
{
    Result<LandingPlatform> Create(string name, Size size);
}
