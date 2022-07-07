using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Abstractions.Factories;

public interface ICoordinatesFactory
{
    RectangularCoordinates Create(Coordinates topLeft, Size size);
}
