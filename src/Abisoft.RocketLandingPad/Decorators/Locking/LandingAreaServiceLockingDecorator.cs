using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Decorators.Locking;

internal class LandingAreaServiceLockingDecorator : LandingAreaLockingDecoratorBase, ILandingAreaService
{
    private readonly ILandingAreaService _decoratedService;

    public LandingAreaServiceLockingDecorator(ILandingAreaService decoratedService)
    {
        _decoratedService = decoratedService;
    }

    public Result<LandingArea> Create(string name, Size size)
    {
        return _decoratedService.Create(name, size);
    }

    public Result AssignLandingPlatform(AssignPlatformRequest request)
    {
        return ExecuteWithSyncLock(request, _decoratedService.AssignLandingPlatform);
    }

    public Result UnassignLandingPlatform(UnassignPlatformRequest request)
    {
        return ExecuteWithSyncLock(request, _decoratedService.UnassignLandingPlatform);
    }
}
