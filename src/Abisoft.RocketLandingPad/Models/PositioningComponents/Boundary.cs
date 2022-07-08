using System.Collections;

namespace Abisoft.RocketLandingPad.Models.PositioningComponents;

public record Boundary(
    Coordinates TopLeft,
    Coordinates TopRight,
    Coordinates BottomLeft,
    Coordinates BottomRight) : IEnumerable<Coordinates>
{
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<Coordinates> GetEnumerator()
    {
        yield return TopLeft;
        yield return TopRight;
        yield return BottomLeft;
        yield return BottomRight;
    }

    public bool Contains(Boundary otherBoundary)
    {
        foreach (var position in otherBoundary)
        {
            if (!IsWithin(position))
            {
                return false;
            }
        }

        return true;
    }

    public bool IsWithin(Coordinates position)
    {
        return TopLeft.Column <= position.Column && position.Column <= TopRight.Column
            && TopLeft.Row <= position.Row && position.Row <= BottomLeft.Row;
    }
}
