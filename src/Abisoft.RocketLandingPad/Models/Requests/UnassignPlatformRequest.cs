using Abisoft.RocketLandingPad.Models.Entities;

namespace Abisoft.RocketLandingPad.Models.Requests;

public class UnassignPlatformRequest
{
    public LandingArea? LandingArea { get; init; }
    public LandingPlatform? Platform { get; init; }
}
