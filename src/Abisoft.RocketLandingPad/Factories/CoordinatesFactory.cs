using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Factories;

public class CoordinatesFactory : ICoordinatesFactory
{
    public Boundary CreateBoundary(Coordinates topLeft, Size size)
    {
        /*
         * top left, R: 0, C: 0
         * size: H: 3, W: 4
         * 
         * [TL]  [  ]  [  ]  [TR]
         * 
         * [  ]  [  ]  [  ]  [  ]
         * 
         * [BL]  [  ]  [  ]  [BR]
        */
        return new(
            TopLeft: new(topLeft.Row, topLeft.Column),
            TopRight: new(topLeft.Row, topLeft.Column + size.Width - 1),
            BottomLeft: new(topLeft.Row + size.Height - 1, topLeft.Column),
            BottomRight: new(topLeft.Row + size.Height - 1, topLeft.Column + size.Width - 1)
        );
    }
}
