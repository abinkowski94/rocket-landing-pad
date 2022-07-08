using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Decorators.Locking;

internal class LandingServiceLockingDecorator : LandingAreaLockingDecoratorBase, ILandingService
{
    private readonly ILandingService _decoratedService;

    public LandingServiceLockingDecorator(ILandingService decoratedService)
    {
        _decoratedService = decoratedService;
    }

    public string CanLandRocketInfo(CanLandRocketRequest request)
    {
        return _decoratedService.CanLandRocketInfo(request);
    }

    public Result CanLandRocket(CanLandRocketRequest request)
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
