using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Services;

internal class LandingAreaService : ILandingAreaService
{
    private readonly IValidator<Size> _sizeValidator;
    private readonly IValidator<AssignPlatformRequest> _assingRequestValidator;
    private readonly IValidator<UnassignPlatformRequest> _unassignRequestValidator;

    private readonly ICoordinatesFactory _coordinatesFactory;
    private readonly ILandingAreaFactory _landingAreaFactory;

    public LandingAreaService(
        IValidator<Size> sizeValidator,
        IValidator<AssignPlatformRequest> assingRequestValidator,
        IValidator<UnassignPlatformRequest> unassignRequestValidator,
        ICoordinatesFactory coordinatesFactory,
        ILandingAreaFactory landingAreaFactory)
    {
        _sizeValidator = sizeValidator;
        _assingRequestValidator = assingRequestValidator;
        _unassignRequestValidator = unassignRequestValidator;

        _coordinatesFactory = coordinatesFactory;
        _landingAreaFactory = landingAreaFactory;
    }

    public Result<LandingArea> Create(string name, Size size)
    {
        var sizeValidationResult = _sizeValidator.Validate(size);
        if (sizeValidationResult is not null)
        {
            return sizeValidationResult;
        }

        return _landingAreaFactory.Create(name, size);
    }

    public async Task<Result> AssignLandingPlatformAsync(AssignPlatformRequest request, CancellationToken cancellationToken)
    {
        var validationResult = _assingRequestValidator.Validate(request);
        if (validationResult is not null)
        {
            return validationResult;
        }

        var rectangularCoordinates = _coordinatesFactory
            .Create(request.Coordinates!, request.Platform!.Size);

        request.LandingArea!.AssignPlatform(request.Platform, rectangularCoordinates);

        return new();
    }

    public async Task<Result> UnassignLandingPlatformAsync(UnassignPlatformRequest request, CancellationToken cancellationToken)
    {
        var validationResult = _unassignRequestValidator.Validate(request);
        if (validationResult is not null)
        {
            return validationResult;
        }

        request.LandingArea!.UnassignPlatform(request.Platform!);

        return new();
    }
}
