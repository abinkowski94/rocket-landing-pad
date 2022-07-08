using Abisoft.RocketLandingPad.Abstractions.Models;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Relations;

namespace Abisoft.RocketLandingPad.Models.Entities;

public class LandingPlatform : IEntity
{
    private readonly List<Rocket> _rockets = new();

    private readonly HashSet<string> _rocketIds = new();

    public string Id { get; }

    public string Name { get; }

    public Size Size { get; }

    public bool HasRockets => Rockets.Count > 0;

    public bool IsAssigned => AssignedArea is not null;

    public bool IsUnassigned => AssignedArea is null;

    public AssignedLandingArea? AssignedArea { get; private set; }

    public IReadOnlyList<Rocket> Rockets => _rockets;

    internal LandingPlatform(
        string id,
        string name,
        Size size)
    {
        Id = id;
        Name = name;
        Size = size;
    }

    internal void AssignToArea(
        LandingArea area,
        Boundary boundary)
    {
        AssignedArea = new(area, boundary);
    }

    internal void UnassignFromArea()
    {
        AssignedArea = null;
    }

    internal IEnumerable<Coordinates> LandRocket(
        Rocket rocket,
        Coordinates position,
        IEnumerable<Coordinates> outline)
    {
        _rocketIds.Add(rocket.Id);
        _rockets.Add(rocket);

        return rocket.Land(this, position, outline);
    }

    internal IEnumerable<Coordinates> StartRocket(Rocket rocket)
    {
        _rocketIds.Remove(rocket.Id);
        _rockets.Remove(rocket);

        return rocket.TakeOff();
    }

    internal bool Contains(Rocket rocket)
    {
        return _rocketIds.Contains(rocket.Id);
    }
}
