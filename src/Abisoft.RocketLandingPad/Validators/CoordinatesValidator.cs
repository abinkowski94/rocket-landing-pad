using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Validators;

internal class CoordinatesValidator : IValidator<Coordinates>
{
    public Exception? Validate(Coordinates value)
    {
        if (value is null)
        {
            return new ArgumentNullException(
                nameof(value),
                $"{nameof(Coordinates)} can not be null.");
        }

        if (value.Row < 0)
        {
            return new ArgumentException(
                $"{nameof(Coordinates)} {nameof(Coordinates.Row)} can not be lower than zero.",
                nameof(value));
        }

        if (value.Column < 0)
        {
            return new ArgumentException(
                $"{nameof(Coordinates)} {nameof(Coordinates.Column)} can not be lower than zero.",
                nameof(value));
        }

        return null;
    }
}
