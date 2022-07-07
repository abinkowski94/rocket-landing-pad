using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Factories;

internal class SquareOutlineFactory : IOutlineFactory
{
    public IEnumerable<Coordinates> Create(Coordinates center)
    {
        /*
         *  [1] [2] [3]
         *  
         *  [4] [C] [5]
         * 
         *  [6] [7] [8]
         */

        // 1
        yield return new(center.Row - 1, center.Column - 1);

        // 2
        yield return new(center.Row - 1, center.Column);

        // 3
        yield return new(center.Row - 1, center.Column + 1);

        // 4
        yield return new(center.Row, center.Column - 1);

        // 5
        yield return new(center.Row, center.Column + 1);

        // 6
        yield return new(center.Row + 1, center.Column - 1);

        // 7
        yield return new(center.Row + 1, center.Column);

        // 8
        yield return new(center.Row + 1, center.Column + 1);
    }
}
