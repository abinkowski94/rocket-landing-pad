using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Abstractions.Factories;

internal interface IOutlineFactory
{
    IEnumerable<Coordinates> Create(Coordinates center);
}
