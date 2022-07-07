using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Validators;

internal class SizeValidator : IValidator<Size>
{
    public Exception? Validate(Size value)
    {
        if (value is null)
        {
            return new ArgumentNullException(
                nameof(value),
                $"{nameof(Size)} can not be null.");
        }

        if (value.Height < 1)
        {
            return new ArgumentException(
                $"{nameof(Size)} {nameof(Size.Height)} can not be lower than one.",
                nameof(value));
        }

        if (value.Width < 1)
        {
            return new ArgumentException(
                $"{nameof(Size)} {nameof(Size.Width)} can not be lower than one.",
                nameof(value));
        }

        return null;
    }
}
