using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Models.Relations;

public record OccupiedPlatform(
    LandingPlatform Platform,
    Coordinates Center,
    IEnumerable<Coordinates> Outline)
{
    public IEnumerable<Coordinates> GetOccupiedCoordinates()
    {
        yield return Center;

        foreach (var outline in Outline)
        {
            yield return outline;
        }
    }
};
