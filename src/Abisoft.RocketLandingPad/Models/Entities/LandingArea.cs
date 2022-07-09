using Abisoft.RocketLandingPad.Abstractions.Models;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Models.Entities;

public class LandingArea : IEntity
{
    private readonly List<LandingPlatform> _platforms = new();

    private readonly HashSet<string> _landingPlatformIds = new();
    private readonly HashSet<Coordinates> _occupiedCoordinates = new();

    internal object SyncRoot { get; } = new();

    public string Id { get; }

    public string Name { get; }

    public Boundary Boundary { get; }

    public bool HasPlatforms => Platforms.Count > 0;

    public bool HasRockets => Rockets.Count > 0;

    public IReadOnlyList<LandingPlatform> Platforms => _platforms;

    public IReadOnlyList<Rocket> Rockets => Platforms.SelectMany(lp => lp.Rockets).ToList();

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
        _platforms.Add(platform);

        platform.AssignToArea(this, boundaries);
    }

    internal void UnassignPlatform(
        LandingPlatform platform)
    {
        _landingPlatformIds.Remove(platform.Id);
        _platforms.Remove(platform);

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
        return _platforms.Any(lp => lp.Contains(rocket));
    }
}
