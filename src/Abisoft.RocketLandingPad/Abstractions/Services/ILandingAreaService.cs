using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Abstractions.Services;

public interface ILandingAreaService
{
    Result<LandingArea> Create(string name, Size size);

    Result AssignLandingPlatform(AssignPlatformRequest request);

    Result UnassignLandingPlatform(UnassignPlatformRequest request);
}
