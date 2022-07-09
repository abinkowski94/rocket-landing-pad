using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Models.Errors;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Decorators.Validating;

internal class LandingServiceValidatingDecorator : ValidatingDecoratorBase, ILandingService
{
    private readonly IValidator<LandRocketRequest> _landRequestValidator;
    private readonly IValidator<StartRocketRequest> _startRequestValidator;

    private readonly ILandingService _decoratedService;

    public LandingServiceValidatingDecorator(
        IValidator<LandRocketRequest> landRequestValidator,
        IValidator<StartRocketRequest> startRequestValidator,
        ILandingService decoratedService)
    {
        _landRequestValidator = landRequestValidator;
        _startRequestValidator = startRequestValidator;

        _decoratedService = decoratedService;
    }

    public string CanLandRocketInfo(LandRocketRequest request)
    {
        var canLandResult = CanLandRocket(request);

        if (canLandResult.IsSuccess)
        {
            return _decoratedService.CanLandRocketInfo(request);
        }
        else if (canLandResult.Error is RocketOutOfPlatformException)
        {
            return Consts.LandingStates.OutOfPlatform;
        }
        else if (canLandResult.Error is RocketClashException)
        {
            return Consts.LandingStates.Clash;
        }
        else
        {
            throw canLandResult.Error!;
        }
    }

    public Result CanLandRocket(LandRocketRequest request)
    {
        return ValidateAndExecuteRequest(
            request,
            _landRequestValidator,
            _decoratedService.CanLandRocket);
    }

    public Result LandRocket(LandRocketRequest request)
    {
        return ValidateAndExecuteRequest(
            request,
            _landRequestValidator,
            _decoratedService.LandRocket);
    }

    public Result StartRocket(StartRocketRequest request)
    {
        return ValidateAndExecuteRequest(
            request,
            _startRequestValidator,
            _decoratedService.StartRocket);
    }
}
