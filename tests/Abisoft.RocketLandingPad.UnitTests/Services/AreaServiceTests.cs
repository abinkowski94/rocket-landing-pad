using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Results;
using Abisoft.RocketLandingPad.Services;

namespace Abisoft.RocketLandingPad.UnitTests.Services;

[Trait("Category", "Unit")]
public class AreaServiceTests
{
    private readonly Mock<IAreaFactory> _areaFactoryMock;
    private readonly AreaService _subject;

    public AreaServiceTests()
    {
        _areaFactoryMock = new(MockBehavior.Strict);
        _subject = new(_areaFactoryMock.Object);
    }

    [Fact]
    public void Create_WhenData_ThenCallsFactory()
    {
        // Arrange
        const string expectedName = "Area 51";
        var expectedSize = new Size(2, 2);
        Result<LandingArea> expectedResult = new LandingArea("1", expectedName, Boundary.From(Coordinates.Zero, expectedSize));

        _areaFactoryMock
            .Setup(f => f.Create(expectedName, expectedSize))
            .Returns(expectedResult.Value!);

        // Act
        var result = _subject.Create(expectedName, expectedSize);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}
