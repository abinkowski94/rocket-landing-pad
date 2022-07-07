using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Models.Relations;
public record AssignedLandingArea(
    LandingArea Area,
    RectangularCoordinates Rectangle);
