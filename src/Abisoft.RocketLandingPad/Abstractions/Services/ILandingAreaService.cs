using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Abstractions.Services;

public interface ILandingAreaService
{
    Result<LandingArea> Create(string name, Size size);

    Task<Result> AssignLandingPlatformAsync(AssignPlatformRequest request, CancellationToken cancellationToken);

    Task<Result> UnassignLandingPlatformAsync(UnassignPlatformRequest request, CancellationToken cancellationToken);
}
