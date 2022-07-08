using Abisoft.RocketLandingPad.Abstractions.Models;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Models.Entities;

public class LandingArea : IEntity
{
    private readonly List<LandingPlatform> _landingPlatforms = new();

    private readonly HashSet<string> _landingPlatformIds = new();
    private readonly HashSet<Coordinates> _occupiedCoordinates = new();

    internal object SyncRoot { get; } = new();

    public string Id { get; }

    public string Name { get; }

    public Boundary Boundary { get; }

    public bool HasPlatforms => LandingPlatforms.Count > 0;

    public bool HasRockets => Rockets.Count > 0;

    public IReadOnlyList<LandingPlatform> LandingPlatforms => _landingPlatforms;

    public IReadOnlyList<Rocket> Rockets => LandingPlatforms.SelectMany(lp => lp.Rockets).ToList();

    public IReadOnlyCollection<Coordinates> OccupiedCoordinates => _occupiedCoordinates;

    internal LandingArea(
        string id,
        string name,
        Boundary boundaries)
    {
        Id = id;
        Name = name;
        Boundary = boundaries;
    }

    internal void AssignPlatform(
        LandingPlatform platform,
        Boundary boundaries)
    {
        _landingPlatformIds.Add(platform.Id);
        _landingPlatforms.Add(platform);

        platform.AssignToArea(this, boundaries);
    }

    internal void UnassignPlatform(
        LandingPlatform platform)
    {
        _landingPlatformIds.Remove(platform.Id);
        _landingPlatforms.Remove(platform);

        platform.UnassignFromArea();
    }

    internal void LandRocket(
        Rocket rocket,
        LandingPlatform platform,
        Coordinates position,
        IEnumerable<Coordinates> outline)
    {
        foreach (var releasedCoordinate in platform.LandRocket(rocket, position, outline))
        {
            _occupiedCoordinates.Add(releasedCoordinate);
        }
    }

    internal void StartRocket(Rocket rocket)
    {
        var platform = rocket.OccupiedPlatform!.Platform;

        foreach (var releasedCoordinate in platform.StartRocket(rocket))
        {
            _occupiedCoordinates.Remove(releasedCoordinate);
        }
    }

    internal bool Contains(LandingPlatform platform)
    {
        return _landingPlatformIds.Contains(platform.Id);
    }

    internal bool Contains(Rocket rocket)
    {
        return _landingPlatforms.Any(lp => lp.Contains(rocket));
    }
}
