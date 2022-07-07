using Abisoft.RocketLandingPad.Abstractions.Models;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Relations;

namespace Abisoft.RocketLandingPad.Models.Entities;

public class Rocket : IEntity
{
    public string Id { get; }

    public string Name { get; }

    public bool IsAirborne => OccupiedPlatform is null;

    public bool IsGrounded => OccupiedPlatform is not null;

    public OccupiedPlatform? OccupiedPlatform { get; private set; }

    internal Rocket(string id, string name)
    {
        Id = id;
        Name = name;
    }

    internal IEnumerable<Coordinates> Land(
        LandingPlatform platform,
        Coordinates center,
        IEnumerable<Coordinates> outline)
    {
        OccupiedPlatform = new(platform, center, outline);

        return OccupiedPlatform.GetOccupiedCoordinates();
    }

    internal IEnumerable<Coordinates> TakeOff()
    {
        var result = OccupiedPlatform?.GetOccupiedCoordinates()
            ?? Enumerable.Empty<Coordinates>();

        OccupiedPlatform = null;

        return result;
    }
}
