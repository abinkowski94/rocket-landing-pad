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

    public bool Contains(Coordinates position)
    {
        return TopLeft.Column <= position.Column && position.Column <= TopRight.Column
            && TopLeft.Row <= position.Row && position.Row <= BottomLeft.Row;
    }

    public bool Contains(Boundary otherBoundary)
    {
        return otherBoundary.All(op => Contains(op));
    }

    public bool Contains(Coordinates position, Size size)
    {
        var otherBoundary = From(position, size);

        return Contains(otherBoundary);
    }

    public bool Overlaps(Boundary otherBoundary)
    {
        return otherBoundary.Any(op => Contains(op));
    }

    public bool Overlaps(Coordinates position, Size size)
    {
        var otherBoundary = From(position, size);

        return Overlaps(otherBoundary);
    }

    public static Boundary From(Coordinates topLeft, Size size)
    {
        /*
         * top left, R: 0, C: 0
         * size: H: 3, W: 4
         * 
         *      0     1     2     3
         *      
         *  0  [TL]  [  ]  [  ]  [TR]
         *   
         *  1  [  ]  [  ]  [  ]  [  ]
         *   
         *  2  [BL]  [  ]  [  ]  [BR]
        */
        return new(
            TopLeft: new(topLeft.Row, topLeft.Column),
            TopRight: new(topLeft.Row, topLeft.Column + size.Width - 1),
            BottomLeft: new(topLeft.Row + size.Height - 1, topLeft.Column),
            BottomRight: new(topLeft.Row + size.Height - 1, topLeft.Column + size.Width - 1)
        );
    }
}
