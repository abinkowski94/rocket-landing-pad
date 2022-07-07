using Abisoft.RocketLandingPad.Abstractions.Models;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Models.Entities;

public class LandingArea : IEntity
{
    private readonly HashSet<string> _landingPlatformIds = new();
    private readonly List<LandingPlatform> _landingPlatforms = new();

    public string Id { get; }

    public string Name { get; }

    public Size Size { get; }

    public bool HasPlatform => LandingPlatforms.Count > 0;

    public bool HasRocket => Rockets.Any();

    public IReadOnlyList<LandingPlatform> LandingPlatforms => _landingPlatforms;

    public IEnumerable<Rocket> Rockets => LandingPlatforms.SelectMany(lp => lp.Rockets);

    internal LandingArea(
        string id,
        string name,
        Size size)
    {
        Id = id;
        Name = name;
        Size = size;
    }

    internal void AssignPlatform(
        LandingPlatform landingPlatform,
        RectangularCoordinates rectangle)
    {
        _landingPlatformIds.Add(landingPlatform.Id);
        _landingPlatforms.Add(landingPlatform);

        landingPlatform.AssignToArea(this, rectangle);
    }

    internal void UnassignPlatform(
        LandingPlatform landingPlatform)
    {
        _landingPlatformIds.Remove(landingPlatform.Id);
        _landingPlatforms.Remove(landingPlatform);

        landingPlatform.UnassignFromArea();
    }

    internal bool Contains(LandingPlatform landingPlatform)
    {
        return _landingPlatformIds.Contains(landingPlatform.Id);
    }

    internal bool Contains(Rocket rocket)
    {
        return _landingPlatforms.Any(lp => lp.Contains(rocket));
    }
}
