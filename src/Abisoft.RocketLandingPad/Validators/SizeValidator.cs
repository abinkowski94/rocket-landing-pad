using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Validators;

internal class SizeValidator : IValidator<Size>
{
    public Exception? Validate(Size? size)
    {
        if (size is null)
        {
            return Consts.Errors.CanNotBeNull(
                nameof(Size),
                nameof(size));
        }

        if (size.Height < 1)
        {
            return Consts.Errors.CanNotBeLowerThanOne(
                nameof(Size),
                nameof(Size.Height),
                nameof(size));
        }

        if (size.Width < 1)
        {
            return Consts.Errors.CanNotBeLowerThanOne(
                nameof(Size),
                nameof(Size.Width),
                nameof(size));
        }

        return null;
    }
}
