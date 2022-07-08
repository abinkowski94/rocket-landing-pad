using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Abstractions.Services;

public interface ILandingService
{
    string CanLandRocketInfo(CanLandRocketRequest request);

    Result CanLandRocket(CanLandRocketRequest request);

    Result LandRocket(LandRocketRequest request);

    Result StartRocket(StartRocketRequest request);
}
