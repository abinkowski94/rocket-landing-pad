using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;
using Abisoft.RocketLandingPad.Services;

namespace Abisoft.RocketLandingPad.UnitTests.Services;

[Trait("Category", "Unit")]
public class LandingServiceTests
{
    private readonly LandingService _subject;

    public LandingServiceTests()
    {
        _subject = new();
    }

    [Fact]
    public void CanLandRocketInfo_WhenRequest_ThenAlwaysReturnsOkForLanding()
    {
        // Arrange
        var request = new LandRocketRequest();

        // Act
        var result = _subject.CanLandRocketInfo(request);

        // Assert
        result.Should().Be(Consts.LandingStates.OkForLanding);
    }

    [Fact]
    public void CanLandRocket_WhenRequest_ThenAlwaysReturnsSuccessResult()
    {
        // Arrange
        var request = new LandRocketRequest();

        // Act
        var result = _subject.CanLandRocket(request);

        // Assert
        result.Should().Be(Result.Success);
    }

    [Fact]
    public void LandRocket_WhenRequest_ThenLandsRocket()
    {
        // Arrange
        const string areaName = "Area 51";
        const string platformName = "Platform one";

        var areaSize = new Size(2, 2);
        var area = new LandingArea("1", areaName, Boundary.From(Coordinates.Zero, areaSize));

        var platformSize = new Size(1, 2);
        var platform = new LandingPlatform("1", platformName, platformSize);

        area.AssignPlatform(platform, Boundary.From(new(1, 0), platformSize));

        var rocket = new Rocket("1", "Tesla rocket");
        var rocketPosition = new Coordinates(1, 1);
        var rocketOutline = new Coordinates[]
        {
            new(0, 0),
            new(0, 1),
            new(1, 0),
        };

        var request = new LandRocketRequest
        {
            Area = area,
            Platform = platform,
            Position = rocketPosition,
            Rocket = rocket,
            Outline = rocketOutline,
        };

        // Act
        var result = _subject.LandRocket(request);

        // Assert
        result.Should().Be(Result.Success);
        area.HasPlatforms.Should().BeTrue();
        area.HasRockets.Should().BeTrue();
        area.OccupiedCoordinates.Should().BeEquivalentTo(request.Outline.Append(rocketPosition));
        area.Contains(platform).Should().BeTrue();
        area.Contains(rocket).Should().BeTrue();

        platform.HasRockets.Should().BeTrue();
        platform.Contains(rocket).Should().BeTrue();

        rocket.IsGrounded.Should().BeTrue();
        rocket.IsAirborne.Should().BeFalse();
        rocket.OccupiedPlatform.Should().NotBeNull();
        rocket.OccupiedPlatform!.Platform.Should().Be(platform);
        rocket.OccupiedPlatform!.Position.Should().Be(rocketPosition);
        rocket.OccupiedPlatform!.Outline.Should().BeEquivalentTo(request.Outline);
    }

    [Fact]
    public void StartRocket_WhenRequest_ThenLandsRocket()
    {
        // Arrange
        const string areaName = "Area 51";
        const string platformName = "Platform one";

        var areaSize = new Size(2, 2);
        var area = new LandingArea("1", areaName, Boundary.From(Coordinates.Zero, areaSize));

        var platformSize = new Size(1, 2);
        var platform = new LandingPlatform("1", platformName, platformSize);

        area.AssignPlatform(platform, Boundary.From(new(1, 0), platformSize));

        var rocket = new Rocket("1", "Tesla rocket");
        var rocketPosition = new Coordinates(1, 1);
        var rocketOutline = new Coordinates[]
        {
            new(0, 0),
            new(0, 1),
            new(1, 0),
        };

        area.LandRocket(rocket, platform, rocketPosition, rocketOutline);

        var request = new StartRocketRequest
        {
            Area = area,
            Rocket = rocket,
        };

        // Act
        var result = _subject.StartRocket(request);

        // Assert
        result.Should().Be(Result.Success);
        area.HasPlatforms.Should().BeTrue();
        area.HasRockets.Should().BeFalse();
        area.OccupiedCoordinates.Should().BeEquivalentTo(Enumerable.Empty<Coordinates>());
        area.Contains(platform).Should().BeTrue();
        area.Contains(rocket).Should().BeFalse();

        platform.HasRockets.Should().BeFalse();
        platform.Contains(rocket).Should().BeFalse();

        rocket.IsGrounded.Should().BeFalse();
        rocket.IsAirborne.Should().BeTrue();
        rocket.OccupiedPlatform.Should().BeNull();
    }
}
