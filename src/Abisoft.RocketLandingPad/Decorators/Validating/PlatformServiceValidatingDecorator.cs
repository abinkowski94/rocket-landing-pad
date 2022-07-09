using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Decorators.Validating;

internal class PlatformServiceValidatingDecorator : ILandingPlatformService
{
    private readonly IValidator<Size> _sizeValidator;

    private readonly ILandingPlatformService _decoratedService;

    public PlatformServiceValidatingDecorator(
        IValidator<Size> sizeValidator,
        ILandingPlatformService decoratedService)
    {
        _sizeValidator = sizeValidator;

        _decoratedService = decoratedService;
    }

    public Result<LandingPlatform> Create(string name, Size size)
    {
        var sizeValidationResult = _sizeValidator.Validate(size);
        if (sizeValidationResult is not null)
        {
            return sizeValidationResult;
        }

        return _decoratedService.Create(name, size);
    }
}
