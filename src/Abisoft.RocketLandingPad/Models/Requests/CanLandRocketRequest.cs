using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Models.Requests;

public class CanLandRocketRequest
{
    public LandingArea? Area { get; init; }
    public Rocket? Rocket {get; init; }
    public Coordinates? Position {get; init; }
}
