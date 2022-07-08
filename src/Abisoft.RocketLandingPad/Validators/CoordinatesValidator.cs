using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Validators;

internal class CoordinatesValidator : IValidator<Coordinates>
{
    public Exception? Validate(Coordinates? coordinates)
    {
        if (coordinates is null)
        {
            return Consts.Errors.CanNotBeNull(
                nameof(Coordinates),
                nameof(coordinates));
        }

        if (coordinates.Row < 0)
        {
            return Consts.Errors.CanNotBeLowerThanZero(
                nameof(Coordinates),
                nameof(Coordinates.Row),
                nameof(coordinates));
        }

        if (coordinates.Column < 0)
        {
            return Consts.Errors.CanNotBeLowerThanZero(
                nameof(Coordinates),
                nameof(Coordinates.Column),
                nameof(coordinates));
        }

        return null;
    }
}
