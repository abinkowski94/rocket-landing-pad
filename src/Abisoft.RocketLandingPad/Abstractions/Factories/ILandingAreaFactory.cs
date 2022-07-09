using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Abstractions.Factories;

internal interface IAreaFactory
{
    LandingArea Create(string name, Size size);
}
