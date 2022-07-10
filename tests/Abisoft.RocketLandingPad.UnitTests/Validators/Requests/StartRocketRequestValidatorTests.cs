using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketStartingPad.Validators.Requests;

namespace Abisoft.RocketLandingPad.UnitTests.Validators.Requests;

[Trait("Category", "Unit")]
public class StartRocketRequestValidatorTests
{
    private readonly StartRocketRequestValidator _subject;

    public StartRocketRequestValidatorTests()
    {
        _subject = new();
    }

    [Theory]
    [MemberData(nameof(GetValidateTestData))]
    public void Validate_WhenData_ThenExpectedResult(StartRocketRequest? request, Exception? expectedResult)
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
                nameof(StartRocketRequest),
                paramName),
        };

        yield return new object?[]
        {
            new StartRocketRequest(),
            Consts.Errors.CanNotBeNull(
                nameof(StartRocketRequest),
                nameof(StartRocketRequest.Area),
                paramName),
        };

        yield return new object?[]
        {
            new StartRocketRequest
            {
                Area = new("1", "Area 51", Boundary.From(Coordinates.Zero, new Size(10, 10))),
            },

            Consts.Errors.CanNotBeNull(
                nameof(StartRocketRequest),
                nameof(StartRocketRequest.Rocket),
                paramName),
        };

        yield return new object?[]
        {
            new StartRocketRequest
            {
                Area = new("1", "Area 51", Boundary.From(Coordinates.Zero, new Size(10, 10))),
                Rocket = new("1", "Rocket one"),
            },

            Consts.Errors.MustBeGrounded(
                nameof(StartRocketRequest),
                nameof(StartRocketRequest.Rocket),
                paramName),
        };

        var area = new LandingArea("1", "Area 51", Boundary.From(Coordinates.Zero, new Size(10, 10)));
        var platform = new LandingPlatform("1", "platform 1", new(4, 4));
        var rocket = new Rocket("1", "Rocket one");

        rocket.Land(new LandingPlatform("2", "other platform", new(5, 5)), new(1, 1), Enumerable.Empty<Coordinates>());

        yield return new object?[]
        {
            new StartRocketRequest
            {
                Area = area,
                Rocket = rocket,
            },

            Consts.Errors.DoesNotContainRocket(
                nameof(StartRocketRequest),
                nameof(StartRocketRequest.Area),
                area.Id,
                rocket.Id,
                paramName),
        };

        area = new LandingArea("1", "Area 51", Boundary.From(Coordinates.Zero, new Size(10, 10)));
        platform = new LandingPlatform("1", "platform 1", new(4, 4));
        rocket = new Rocket("1", "Rocket one");

        area.AssignPlatform(platform, Boundary.From(Coordinates.Zero, platform.Size));
        area.LandRocket(rocket, platform, new(1,1), Enumerable.Empty<Coordinates>());

        yield return new object?[]
        {
            new StartRocketRequest
            {
                Area = area,
                Rocket = rocket,
            },
            null,
        };
    }
}
