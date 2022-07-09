using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Decorators.ThreadLocking;

internal class LandingServiceLockingDecorator : AreaLockingDecoratorBase, ILandingService
{
    private readonly ILandingService _decoratedService;

    public LandingServiceLockingDecorator(ILandingService decoratedService)
    {
        _decoratedService = decoratedService;
    }

    public string CanLandRocketInfo(LandRocketRequest request)
    {
        return _decoratedService.CanLandRocketInfo(request);
    }

    public Result CanLandRocket(LandRocketRequest request)
    {
        return ExecuteWithSyncLock(request, _decoratedService.CanLandRocket);
    }

    public Result LandRocket(LandRocketRequest request)
    {
        return ExecuteWithSyncLock(request, _decoratedService.LandRocket);
    }

    public Result StartRocket(StartRocketRequest request)
    {
        return ExecuteWithSyncLock(request, _decoratedService.StartRocket);
    }
}
