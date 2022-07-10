using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;
using Abisoft.RocketLandingPad.Services;

namespace Abisoft.RocketLandingPad.UnitTests.Services;

[Trait("Category", "Unit")]
public class AreaServiceTests : IDisposable
{
    private readonly Mock<IAreaFactory> _areaFactoryMock;
    private readonly AreaService _subject;

    public AreaServiceTests()
    {
        _areaFactoryMock = new(MockBehavior.Strict);
        _subject = new(_areaFactoryMock.Object);
    }

    public void Dispose()
    {
        _areaFactoryMock.VerifyAll();

        GC.SuppressFinalize(this);
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

    [Fact]
    public void AssignLandingPlatform_WhenData_ThenAssignsPlatformToArea()
    {
        // Arrange
        const string areaName = "Area 51";
        const string platformName = "Platform one";

        var areaSize = new Size(2, 2);
        var area = new LandingArea("1", areaName, Boundary.From(Coordinates.Zero, areaSize));

        var platformSize = new Size(1, 2);
        var platform = new LandingPlatform("1", platformName, platformSize);

        var position = new Coordinates(1, 0);

        var request = new AssignPlatformRequest
        {
            Area = area,
            Platform = platform,
            Position = position,
        };

        var expectedPlatformBoundary = Boundary.From(position, platformSize);

        // Act
        var result = _subject.AssignLandingPlatform(request);

        // Assert
        result.Should().BeEquivalentTo(Result.Success);
        area.HasPlatforms.Should().BeTrue();
        area.Contains(platform).Should().BeTrue();
        platform.AssignedArea.Should().NotBeNull();
        platform.AssignedArea!.Area.Should().Be(area);
        platform.AssignedArea.Boundary.Should().BeEquivalentTo(expectedPlatformBoundary);
    }

    [Fact]
    public void UnassignLandingPlatform_WhenData_ThenUnassignsPlatformFromArea()
    {
        // Arrange
        const string areaName = "Area 51";
        const string platformName = "Platform one";

        var areaSize = new Size(2, 2);
        var area = new LandingArea("1", areaName, Boundary.From(Coordinates.Zero, areaSize));

        var platformSize = new Size(1, 2);
        var platform = new LandingPlatform("1", platformName, platformSize);

        var position = new Coordinates(1, 0);

        var assignRequest = new AssignPlatformRequest
        {
            Area = area,
            Platform = platform,
            Position = position,
        };

        _subject.AssignLandingPlatform(assignRequest);

        var request = new UnassignPlatformRequest
        {
            Area = area,
            Platform = platform,
        };

        // Act
        var result = _subject.UnassignLandingPlatform(request);

        // Assert
        result.Should().BeEquivalentTo(Result.Success);
        area.HasPlatforms.Should().BeFalse();
        area.Contains(platform).Should().BeFalse();
        platform.AssignedArea.Should().BeNull();
    }
}
