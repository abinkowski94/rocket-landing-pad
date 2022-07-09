using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Abstractions.Factories;

internal interface IOutlineFactory
{
    IReadOnlyCollection<Coordinates> Create(Boundary boundary, Coordinates postion);
}
