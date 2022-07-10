using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.UnitTests.Models.PositioningComponents;

[Trait("Category", "Unit")]
public class BoundaryTests
{
    [Fact]
    public void GetEnumeratorObject_WhenData_ThenReturnsInExpectedOrder()
    {
        // Arrange
        var boundary = new Boundary(
            new Coordinates(0, 0),
            new Coordinates(0, 9),
            new Coordinates(9, 0),
            new Coordinates(9, 9)
        );

        var expectedResult = new Coordinates[]
        {
            new Coordinates(0, 0),
            new Coordinates(0, 9),
            new Coordinates(9, 0),
            new Coordinates(9, 9),
        };

        // Act
        System.Collections.IEnumerable result = boundary;

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void GetEnumerator_WhenData_ThenReturnsInExpectedOrder()
    {
        // Arrange
        var boundary = new Boundary(
            new Coordinates(0, 0),
            new Coordinates(0, 9),
            new Coordinates(9, 0),
            new Coordinates(9, 9)
        );

        var expectedResult = new Coordinates[]
        {
            new Coordinates(0, 0),
            new Coordinates(0, 9),
            new Coordinates(9, 0),
            new Coordinates(9, 9),
        };

        // Act
        var result = boundary.AsEnumerable();

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Theory]
    [MemberData(nameof(GetContainsCoordinatesTestData))]
    public void Contains_WhenCoordinates_ThenExpectedResult(
        Boundary boundary,
        Coordinates coordinates,
        bool expectedResult)
    {
        // Act
        var result = boundary.Contains(coordinates);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [MemberData(nameof(GetContainsBoundaryTestData))]
    public void Contains_WhenBoundary_ThenExpectedResult(
        Boundary boundary,
        Boundary otherBoundary,
        bool expectedResult)
    {
        // Act
        var result = boundary.Contains(otherBoundary);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void Contains_WhenCoordinatesAndSize_ThenExpectedResult()
    {
        // Arrange
        var boundary = new Boundary(
            new Coordinates(2, 2),
            new Coordinates(2, 8),
            new Coordinates(8, 2),
            new Coordinates(8, 8)
        );

        // Act
        var result = boundary.Contains(new(3, 3), new(2, 2));

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(GetOverlapsBoundaryTestData))]
    public void Overlaps_WhenBoundary_ThenExpectedResult(
        Boundary boundary,
        Boundary otherBoundary,
        bool expectedResult)
    {
        // Act
        var result = boundary.Overlaps(otherBoundary);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Fact]
    public void Overlaps_WhenCoordinatesAndSize_ThenExpectedResult()
    {
        // Arrange
        var boundary = new Boundary(
            new Coordinates(2, 2),
            new Coordinates(2, 8),
            new Coordinates(8, 2),
            new Coordinates(8, 8)
        );

        // Act
        var result = boundary.Overlaps(new(3, 3), new(2, 2));

        // Assert
        result.Should().BeTrue();
    }

    public static IEnumerable<object?[]> GetContainsCoordinatesTestData()
    {
        var boundary = new Boundary(
            new Coordinates(1, 1),
            new Coordinates(1, 8),
            new Coordinates(8, 1),
            new Coordinates(8, 8)
        );

        yield return new object?[]
        {
            boundary,
            new Coordinates(0, 0),
            false,
        };

        yield return new object?[]
        {
            boundary,
            new Coordinates(9, 9),
            false,
        };

        yield return new object?[]
        {
            boundary,
            new Coordinates(1, 1),
            true,
        };

        yield return new object?[]
        {
            boundary,
            new Coordinates(8, 8),
            true,
        };

        yield return new object?[]
        {
            boundary,
            new Coordinates(1, 8),
            true,
        };

        yield return new object?[]
        {
            boundary,
            new Coordinates(8, 1),
            true,
        };
    }

    public static IEnumerable<object?[]> GetContainsBoundaryTestData()
    {
        var boundary = new Boundary(
            new Coordinates(2, 2),
            new Coordinates(2, 8),
            new Coordinates(8, 2),
            new Coordinates(8, 8)
        );

        yield return new object?[]
        {
            boundary,
            new Boundary(
                new Coordinates(0, 0),
                new Coordinates(0, 1),
                new Coordinates(1, 0),
                new Coordinates(1, 1)
            ),
            false,
        };

        yield return new object?[]
        {
            boundary,
            new Boundary(
                new Coordinates(0, 0),
                new Coordinates(0, 3),
                new Coordinates(3, 0),
                new Coordinates(3, 3)
            ),
            false,
        };

        yield return new object?[]
        {
            boundary,
            new Boundary(
                new Coordinates(3, 3),
                new Coordinates(3, 9),
                new Coordinates(9, 3),
                new Coordinates(9, 9)
            ),
            false,
        };

        yield return new object?[]
        {
            boundary,
            new Boundary(
                new Coordinates(3, 3),
                new Coordinates(3, 4),
                new Coordinates(4, 3),
                new Coordinates(4, 4)
            ),
            true,
        };
    }

    public static IEnumerable<object?[]> GetOverlapsBoundaryTestData()
    {
        var boundary = new Boundary(
            new Coordinates(2, 2),
            new Coordinates(2, 8),
            new Coordinates(8, 2),
            new Coordinates(8, 8)
        );

        yield return new object?[]
        {
            boundary,
            new Boundary(
                new Coordinates(0, 0),
                new Coordinates(0, 1),
                new Coordinates(1, 0),
                new Coordinates(1, 1)
            ),
            false,
        };

        yield return new object?[]
        {
            boundary,
            new Boundary(
                new Coordinates(0, 0),
                new Coordinates(0, 3),
                new Coordinates(3, 0),
                new Coordinates(3, 3)
            ),
            true,
        };

        yield return new object?[]
        {
            boundary,
            new Boundary(
                new Coordinates(3, 3),
                new Coordinates(3, 9),
                new Coordinates(9, 3),
                new Coordinates(9, 9)
            ),
            true,
        };

        yield return new object?[]
        {
            boundary,
            new Boundary(
                new Coordinates(3, 3),
                new Coordinates(3, 4),
                new Coordinates(4, 3),
                new Coordinates(4, 4)
            ),
            true,
        };
    }
}
