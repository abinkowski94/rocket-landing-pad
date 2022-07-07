using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Models.Requests;

public class LandRocketRequest
{
    public Rocket? Rocket { get; init; }

    public Coordinates? Position { get; init; }
}
