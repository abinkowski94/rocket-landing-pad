using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Factories;

internal class SquareOutlineFactory : IOutlineFactory
{
    public IReadOnlyCollection<Coordinates> Create(Boundary boundary, Coordinates position)
    {
        return Create(position)
            .Where(p => boundary.Contains(p))
            .ToList();
    }

    private static IEnumerable<Coordinates> Create(Coordinates position)
    {
        /*
         *  [1] [2] [3]
         *  
         *  [4] [C] [5]
         * 
         *  [6] [7] [8]
         */

        // 1
        yield return new(position.Row - 1, position.Column - 1);

        // 2
        yield return new(position.Row - 1, position.Column);

        // 3
        yield return new(position.Row - 1, position.Column + 1);

        // 4
        yield return new(position.Row, position.Column - 1);

        // 5
        yield return new(position.Row, position.Column + 1);

        // 6
        yield return new(position.Row + 1, position.Column - 1);

        // 7
        yield return new(position.Row + 1, position.Column);

        // 8
        yield return new(position.Row + 1, position.Column + 1);
    }
}
