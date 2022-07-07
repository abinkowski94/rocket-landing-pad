using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Services;

internal class LandingPlatformService : ILandingPlatformService
{
    private readonly IValidator<Size> _sizeValidator;
    private readonly ILandingPlatformFactory _landingPlatformFactory;

    public LandingPlatformService(
        IValidator<Size> sizeValidator,
        ILandingPlatformFactory landingPlatformFactory)
    {
        _sizeValidator = sizeValidator;
        _landingPlatformFactory = landingPlatformFactory;
    }

    public Result<LandingPlatform> Create(string name, Size size)
    {
        var sizeValidationResult = _sizeValidator.Validate(size);
        if (sizeValidationResult is not null)
        {
            return sizeValidationResult;
        }

        return _landingPlatformFactory.Create(name, size);
    }
}
