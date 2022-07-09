using Abisoft.RocketLandingPad.Factories;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.UnitTests.Factories;

[Trait("Category", "Unit")]
public class SquareOutlineFactoryTest
{
    private readonly SquareOutlineFactory _subject;

    public SquareOutlineFactoryTest()
    {
        _subject = new();
    }

    [Theory]
    [MemberData(nameof(GetCreateTestData))]
    public void Create_WhenData_ThenReturnsExpectedResult(Boundary boundary, Coordinates position, Coordinates[] expectedResult)
    {
        // Act
        var result = _subject.Create(boundary, position);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    public static IEnumerable<object?[]> GetCreateTestData()
    {
        // Center
        yield return new object?[]
        {
            Boundary.From(Coordinates.Zero, new Size(5, 5)),
            new Coordinates(2, 2),
            new[]
            {
                new Coordinates(1, 1),
                new Coordinates(1, 2),
                new Coordinates(1, 3),
                new Coordinates(2, 1),
                new Coordinates(2, 3),
                new Coordinates(3, 1),
                new Coordinates(3, 2),
                new Coordinates(3, 3),
            },
        };

        // Top left
        yield return new object?[]
        {
            Boundary.From(Coordinates.Zero, new Size(5, 5)),
            new Coordinates(0, 0),
            new[]
            {
                new Coordinates(0, 1),
                new Coordinates(1, 0),
                new Coordinates(1, 1),
            },
        };

        // Top right
        yield return new object?[]
        {
            Boundary.From(Coordinates.Zero, new Size(5, 5)),
            new Coordinates(0, 4),
            new[]
            {
                new Coordinates(0, 3),
                new Coordinates(1, 3),
                new Coordinates(1, 4),
            },
        };

        // Bottom left
        yield return new object?[]
        {
            Boundary.From(Coordinates.Zero, new Size(5, 5)),
            new Coordinates(4, 0),
            new[]
            {
                new Coordinates(3, 0),
                new Coordinates(3, 1),
                new Coordinates(4, 1),
            },
        };

        // Bottom right
        yield return new object?[]
        {
            Boundary.From(Coordinates.Zero, new Size(5, 5)),
            new Coordinates(4, 4),
            new[]
            {
                new Coordinates(3, 3),
                new Coordinates(3, 4),
                new Coordinates(4, 3),
            },
        };
    }
}
