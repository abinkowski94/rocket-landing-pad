using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;
using Abisoft.RocketLandingPad.Validators.Requests;

namespace Abisoft.RocketLandingPad.UnitTests.Validators.Requests;

[Trait("Category", "Unit")]
public class AssignPlatformRequestValidatorTests
{
    private readonly Mock<IValidator<Coordinates>> _coordinatesValidatorMock;
    private readonly AssignPlatformRequestValidator _subject;

    public AssignPlatformRequestValidatorTests()
    {
        _coordinatesValidatorMock = new();
        _subject = new(_coordinatesValidatorMock.Object);
    }

    [Theory]
    [MemberData(nameof(GetValidateTestData))]
    public void Validate_WhenData_ThenExpectedResult(AssignPlatformRequest? request, Exception? expectedResult)
    {
        // Act
        var result = _subject.Validate(request);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void Validate_WhenCoordinatesAreInvalid_ThenReturnsInvalidCoordinates()
    {
        // Arrange
        var request = new AssignPlatformRequest
        {
            Area = new("1", "area", Boundary.From(Coordinates.Zero, new(2, 2))),
            Platform = new("1", "platform", new(1, 1)),
            Position = new(-1, -1),
        };

        var expectedResult = new Exception("Invalid coordinates");

        _coordinatesValidatorMock
            .Setup(v => v.Validate(request.Position))
            .Returns(expectedResult);

        // Act
        var result = _subject.Validate(request);

        // Assert
        result.Should().Be(expectedResult);
    }

    public static IEnumerable<object?[]> GetValidateTestData()
    {
        const string paramName = "request";

        yield return new object?[]
        {
            null,
            Consts.Errors.CanNotBeNull(nameof(AssignPlatformRequest), paramName),
        };

        yield return new object?[]
        {
            new AssignPlatformRequest(),
            Consts.Errors.CanNotBeNull(nameof(AssignPlatformRequest), nameof(AssignPlatformRequest.Area), paramName),
        };

        yield return new object?[]
        {
            new AssignPlatformRequest
            {
                Area = new("1", "area", Boundary.From(Coordinates.Zero, new(3, 3))),
            },
            Consts.Errors.CanNotBeNull(nameof(AssignPlatformRequest), nameof(AssignPlatformRequest.Platform), paramName),
        };

        yield return new object?[]
        {
            new AssignPlatformRequest
            {
                Area = new("1", "area", Boundary.From(Coordinates.Zero, new(2, 2))),
                Platform = new("1", "platform", new(1,1)),
            },
            Consts.Errors.CanNotBeNull(nameof(AssignPlatformRequest), nameof(AssignPlatformRequest.Position), paramName),
        };

        var area = new LandingArea("1", "area", Boundary.From(Coordinates.Zero, new(2, 2)));
        var platform = new LandingPlatform("1", "platform", new(1, 1));
        var position = new Coordinates(1, 1);

        area.AssignPlatform(platform, Boundary.From(position, platform.Size));

        yield return new object?[]
        {
            new AssignPlatformRequest
            {
                Area = area,
                Platform = platform,
                Position = position,
            },

            Consts.Errors.AlreadyContainsPlatform(
                nameof(AssignPlatformRequest),
                nameof(AssignPlatformRequest.Area),
                platform.Id,
                paramName),
        };

        area = new LandingArea("1", "area", Boundary.From(Coordinates.Zero, new(2, 2)));
        platform = new LandingPlatform("1", "platform", new(1, 1));
        position = new Coordinates(6, 6);

        yield return new object?[]
        {
            new AssignPlatformRequest
            {
                Area = area,
                Platform = platform,
                Position = position,
            },

            Consts.Errors.DoesNotFitInsideArea(
                nameof(AssignPlatformRequest),
                nameof(AssignPlatformRequest.Platform),
                platform.Id,
                area.Id,
                paramName),
        };

        area = new LandingArea("1", "area", Boundary.From(Coordinates.Zero, new(2, 2)));
        platform = new LandingPlatform("1", "platform", new(2, 2));
        position = new Coordinates(0, 0);

        area.AssignPlatform(platform, Boundary.From(position, platform.Size));

        var newPlatform = new LandingPlatform("2", "new platform", new(1, 1));
        var newPosition = new Coordinates(1, 1);

        yield return new object?[]
        {
            new AssignPlatformRequest
            {
                Area = area,
                Platform = newPlatform,
                Position = newPosition,
            },

            Consts.Errors.OverlapsWithExistingPlatform(
                nameof(AssignPlatformRequest),
                nameof(AssignPlatformRequest.Platform),
                newPlatform.Id,
                platform.Id,
                paramName),
        };

        area = new LandingArea("1", "area", Boundary.From(Coordinates.Zero, new(2, 2)));
        platform = new LandingPlatform("1", "platform", new(2, 2));
        position = new Coordinates(0, 0);

        yield return new object?[]
        {
            new AssignPlatformRequest
            {
                Area = area,
                Platform = platform,
                Position = position,
            },

            null,
        };
    }
}
