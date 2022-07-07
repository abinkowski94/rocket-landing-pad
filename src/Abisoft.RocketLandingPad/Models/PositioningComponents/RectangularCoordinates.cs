namespace Abisoft.RocketLandingPad.Models.PositioningComponents;

public record RectangularCoordinates(
    Coordinates TopLeft,
    Coordinates TopRight,
    Coordinates BottomLeft,
    Coordinates BottomRight);
