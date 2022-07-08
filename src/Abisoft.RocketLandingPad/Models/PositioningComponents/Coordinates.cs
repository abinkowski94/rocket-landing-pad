namespace Abisoft.RocketLandingPad.Models.PositioningComponents;

public record Coordinates(int Row, int Column)
{
    public static Coordinates Zero { get; } = new(0, 0);
}
