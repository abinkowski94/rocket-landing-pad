using Abisoft.RocketLandingPad.Abstractions.Models;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Relations;

namespace Abisoft.RocketLandingPad.Models.Entities;

public class LandingPlatform : IEntity
{
    private readonly HashSet<string> _rocketIds = new();
    private readonly List<Rocket> _rockets = new();
    private readonly HashSet<Coordinates> _occupiedCoordinates = new();

    public string Id { get; }

    public string Name { get; }

    public Size Size { get; }

    public bool IsAssigned => AssignedLandingArea is not null;

    public bool IsUnassigned => !IsAssigned;

    public AssignedLandingArea? AssignedLandingArea { get; private set; }

    public IReadOnlyList<Rocket> Rockets => _rockets;

    public IReadOnlyCollection<Coordinates> OccupiedCoordinates => _occupiedCoordinates;

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
        LandingArea landingArea,
        RectangularCoordinates rectangle)
    {
        AssignedLandingArea = new(landingArea, rectangle);
    }

    internal void UnassignFromArea()
    {
        AssignedLandingArea = null;
    }

    internal void LandRocket(
        Rocket rocket,
        Coordinates center,
        IEnumerable<Coordinates> outline)
    {
        _rocketIds.Add(rocket.Id);
        _rockets.Add(rocket);

        foreach (var releasedCoordinate in rocket.Land(this, center, outline))
        {
            _occupiedCoordinates.Add(releasedCoordinate);
        }
    }

    internal void StartRocket(
        Rocket rocket)
    {
        _rocketIds.Remove(rocket.Id);
        _rockets.Remove(rocket);

        foreach (var releasedCoordinate in rocket.TakeOff())
        {
            _occupiedCoordinates.Remove(releasedCoordinate);
        }
    }

    internal bool Contains(Rocket rocket)
    {
        return _rocketIds.Contains(rocket.Id);
    }
}
