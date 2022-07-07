using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Abstractions.Services;

public interface ILandingService
{
    string CanLandRocketInfo(CanLandRocketRequest request);

    Result CanLandRocket(CanLandRocketRequest request);

    Task<Result> LandRocketAsync(LandRocketRequest request, CancellationToken cancellationToken);

    Task<Result> StartRocketAsync(StartRocketRequest request, CancellationToken cancellationToken);
}
