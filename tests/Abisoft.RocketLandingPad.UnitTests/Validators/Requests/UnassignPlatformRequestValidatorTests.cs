using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Validators.Requests;

namespace Abisoft.RocketLandingPad.UnitTests.Validators.Requests;

[Trait("Category", "Unit")]
public class UnassignPlatformRequestValidatorTests
{
    private readonly UnassignPlatformRequestValidator _subject;

    public UnassignPlatformRequestValidatorTests()
    {
        _subject = new();
    }

    [Theory]
    [MemberData(nameof(GetValidateTestData))]
    public void Validate_WhenData_ThenExpectedResult(UnassignPlatformRequest? request, Exception? expectedResult)
    {
        // Act
        var result = _subject.Validate(request);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    public static IEnumerable<object?[]> GetValidateTestData()
    {
        const string paramName = "request";

        yield return new object?[]
        {
            null,
            Consts.Errors.CanNotBeNull(
                nameof(UnassignPlatformRequest),
                paramName),
        };

        yield return new object?[]
        {
            new UnassignPlatformRequest(),
            Consts.Errors.CanNotBeNull(
                nameof(UnassignPlatformRequest),
                nameof(UnassignPlatformRequest.Area),
                paramName),
        };

        yield return new object?[]
        {
            new UnassignPlatformRequest
            {
                Area = new("1", "Area 51", Boundary.From(Coordinates.Zero, new(10, 10))),
            },

            Consts.Errors.CanNotBeNull(
                nameof(UnassignPlatformRequest),
                nameof(UnassignPlatformRequest.Platform),
                paramName),
        };

        yield return new object?[]
        {
            new UnassignPlatformRequest
            {
                Area = new("1", "Area 51", Boundary.From(Coordinates.Zero, new(10, 10))),
                Platform = new("1", "Tesla", new(2,2)),
            },

            Consts.Errors.PlatformAlreadyUnassigned(
                nameof(UnassignPlatformRequest),
                nameof(UnassignPlatformRequest.Platform),
                paramName),
        };

        var area = new LandingArea("1", "Area 51", Boundary.From(Coordinates.Zero, new(10, 10)));
        var platform = new LandingPlatform("1", "Tesla", new(2, 2));

        var otherArea = new LandingArea("2", "Other area", Boundary.From(Coordinates.Zero, new(10, 10)));
        otherArea.AssignPlatform(platform, Boundary.From(Coordinates.Zero, platform.Size));

        yield return new object?[]
        {
            new UnassignPlatformRequest
            {
                Area = area,
                Platform = platform,
            },

            Consts.Errors.DoesNotContainPlatform(
                nameof(UnassignPlatformRequest),
                nameof(UnassignPlatformRequest.Area),
                platform.Id,
                paramName),
        };

        area = new LandingArea("1", "Area 51", Boundary.From(Coordinates.Zero, new(10, 10)));
        platform = new LandingPlatform("1", "Tesla", new(2, 2));

        area.AssignPlatform(platform, Boundary.From(Coordinates.Zero, platform.Size));
        area.LandRocket(new("1", "R1"), platform, new(1, 1), Enumerable.Empty<Coordinates>());

        yield return new object?[]
        {
            new UnassignPlatformRequest
            {
                Area = area,
                Platform = platform,
            },

            Consts.Errors.StillContainsRockets(
                nameof(UnassignPlatformRequest),
                nameof(UnassignPlatformRequest.Platform),
                paramName),
        };

        area = new LandingArea("1", "Area 51", Boundary.From(Coordinates.Zero, new(10, 10)));
        platform = new LandingPlatform("1", "Tesla", new(2, 2));

        area.AssignPlatform(platform, Boundary.From(Coordinates.Zero, platform.Size));

        yield return new object?[]
        {
            new UnassignPlatformRequest
            {
                Area = area,
                Platform = platform,
            },

            null,
        };
    }
}
