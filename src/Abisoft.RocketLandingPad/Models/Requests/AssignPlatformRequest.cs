using Abisoft.RocketLandingPad.Abstractions.Models;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Models.Requests;

public class AssignPlatformRequest : ILandingAreaContainer
{
    public LandingArea? Area { get; init; }

    public LandingPlatform? Platform { get; init; }
    public Coordinates? Position { get; init; }
}
