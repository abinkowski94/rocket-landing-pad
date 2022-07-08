using Abisoft.RocketLandingPad.Abstractions.Models;
using Abisoft.RocketLandingPad.Models.Entities;

namespace Abisoft.RocketLandingPad.Models.Requests;

public class UnassignPlatformRequest : ILandingAreaContainer
{
    public LandingArea? Area { get; init; }

    public LandingPlatform? Platform { get; init; }
}
