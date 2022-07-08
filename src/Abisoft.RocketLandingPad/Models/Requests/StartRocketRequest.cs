using Abisoft.RocketLandingPad.Abstractions.Models;
using Abisoft.RocketLandingPad.Models.Entities;

namespace Abisoft.RocketLandingPad.Models.Requests;

public class StartRocketRequest : ILandingAreaContainer
{
    public LandingArea? Area { get; init; }

    public Rocket? Rocket { get; init; }
}
