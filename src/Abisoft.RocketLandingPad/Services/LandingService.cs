using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Services;

internal class LandingService : ILandingService
{
    public string CanLandRocketInfo(LandRocketRequest request)
    {
        return Consts.LandingStates.OkForLanding;
    }

    public Result CanLandRocket(LandRocketRequest request)
    {
        return new Result();
    }

    public Result LandRocket(LandRocketRequest request)
    {
        request.Area!.LandRocket(
            request.Rocket!,
            request.Platform!,
            request.Position!,
            request.Outline!);

        return new Result();
    }

    public Result StartRocket(StartRocketRequest request)
    {
        request.Area!.StartRocket(request.Rocket!);

        return new Result();
    }
}
