using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Decorators.Validating;

internal class AreaServiceValidatingDecorator : ValidatingDecoratorBase, ILandingAreaService
{
    private readonly IValidator<Size> _sizeValidator;
    private readonly IValidator<AssignPlatformRequest> _assingRequestValidator;
    private readonly IValidator<UnassignPlatformRequest> _unassignRequestValidator;

    private readonly ILandingAreaService _decoratedService;

    public AreaServiceValidatingDecorator(
        IValidator<Size> sizeValidator,
        IValidator<AssignPlatformRequest> assingRequestValidator,
        IValidator<UnassignPlatformRequest> unassignRequestValidator,
        ILandingAreaService decoratedService)
    {
        _sizeValidator = sizeValidator;
        _assingRequestValidator = assingRequestValidator;
        _unassignRequestValidator = unassignRequestValidator;

        _decoratedService = decoratedService;
    }

    public Result<LandingArea> Create(string name, Size size)
    {
        var validationResult = _sizeValidator.Validate(size);
        if (validationResult is not null)
        {
            return validationResult;
        }

        return _decoratedService.Create(name, size);
    }

    public Result AssignLandingPlatform(AssignPlatformRequest request)
    {
        return ValidateAndExecuteRequest(
            request,
            _assingRequestValidator,
            _decoratedService.AssignLandingPlatform);
    }

    public Result UnassignLandingPlatform(UnassignPlatformRequest request)
    {
        return ValidateAndExecuteRequest(
            request,
            _unassignRequestValidator,
            _decoratedService.UnassignLandingPlatform);
    }
}
