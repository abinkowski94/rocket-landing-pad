using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Abstractions.Factories;

public interface ICoordinatesFactory
{
    Boundary CreateBoundary(Coordinates topLeft, Size size);
}
