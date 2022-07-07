using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Services;

internal class LandingService : ILandingService
{
    private readonly IValidator<CanLandRocketRequest> _canLandRequestValidator;
    private readonly IValidator<LandRocketRequest> _landRequestValidator;
    private readonly IValidator<StartRocketRequest> _startRequestValidator;

    private readonly IOutlineFactory _outlineFactory;

    public LandingService(
        IValidator<CanLandRocketRequest> canLandRequestValidator,
        IValidator<LandRocketRequest> landRequestValidator,
        IValidator<StartRocketRequest> startRequestValidator,
        IOutlineFactory outlineFactory)
    {
        _canLandRequestValidator = canLandRequestValidator;
        _landRequestValidator = landRequestValidator;
        _startRequestValidator = startRequestValidator;

        _outlineFactory = outlineFactory;
    }

    public string CanLandRocketInfo(CanLandRocketRequest request)
    {
        var canLandResult = CanLandRocket(request);
        if (canLandResult.IsSuccess)
        {
            return "ok for landing";
        }

        return canLandResult.Error!.Message;
    }

    public Result CanLandRocket(CanLandRocketRequest request)
    {
        var validationResult = _canLandRequestValidator.Validate(request);
        if (validationResult is not null)
        {
            return validationResult;
        }

        throw new NotImplementedException();
    }

    public async Task<Result> LandRocketAsync(LandRocketRequest request, CancellationToken cancellationToken)
    {
        var validationResult = _landRequestValidator.Validate(request);
        if (validationResult is not null)
        {
            return validationResult;
        }

        throw new NotImplementedException();
    }

    public async Task<Result> StartRocketAsync(StartRocketRequest request, CancellationToken cancellationToken)
    {
        var validationResult = _startRequestValidator.Validate(request);
        if (validationResult is not null)
        {
            return validationResult;
        }

        throw new NotImplementedException();
    }
}
