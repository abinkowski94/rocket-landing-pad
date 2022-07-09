using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Abstractions.Factories;

internal interface IPlatformFactory
{
    LandingPlatform Create(string name, Size size);
}
