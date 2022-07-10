using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Validators.Requests;

namespace Abisoft.RocketLandingPad.UnitTests.Validators.Requests;

[Trait("Category", "Unit")]
public class LandRocketRequestValidatorTests
{
    private readonly Mock<IValidator<Coordinates>> _coordinatesValidatorMock;
    private readonly Mock<IOutlineFactory> _outlineFactoryMock;

    private readonly LandRocketRequestValidator _subject;

    public LandRocketRequestValidatorTests()
    {
        _coordinatesValidatorMock = new();
        _outlineFactoryMock = new();

        _subject = new(
            _coordinatesValidatorMock.Object,
            _outlineFactoryMock.Object);
    }

    [Theory]
    [MemberData(nameof(GetValidateTestData))]
    public void Validate_WhenData_ThenExpectedResult(
        LandRocketRequest? request,
        Exception? coordinatesValidationResult,
        IReadOnlyList<Coordinates> outlineResult,
        Exception? expectedResult)
    {
        // Arrange
        var postion = request?.Position;

        _coordinatesValidatorMock
            .Setup(v => v.Validate(postion))
            .Returns(coordinatesValidationResult);

        _outlineFactoryMock
            .Setup(f => f.Create(It.IsAny<Boundary>(), postion!))
            .Returns(outlineResult);

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
            null,
            null,
            Consts.Errors.CanNotBeNull(
                nameof(LandRocketRequest),
                paramName),
        };

        yield return new object?[]
        {
            new LandRocketRequest(),
            null,
            null,
            Consts.Errors.CanNotBeNull(
                nameof(LandRocketRequest),
                nameof(LandRocketRequest.Area),
                paramName),
        };

        yield return new object?[]
        {
            new LandRocketRequest
            {
                Area = new("1", "Area 51", Boundary.From(Coordinates.Zero, new Size(10, 10))),
            },
            null,
            null,
            Consts.Errors.CanNotBeNull(
                nameof(LandRocketRequest),
                nameof(LandRocketRequest.Rocket),
                paramName),
        };

        yield return new object?[]
        {
            new LandRocketRequest
            {
                Area = new("1", "Area 51", Boundary.From(Coordinates.Zero, new Size(10, 10))),
                Rocket = new("1", "R1"),
            },
            null,
            null,
            Consts.Errors.CanNotBeNull(
                nameof(LandRocketRequest),
                nameof(LandRocketRequest.Position),
                paramName),
        };

        yield return new object?[]
        {
            new LandRocketRequest
            {
                Area = new("1", "Area 51", Boundary.From(Coordinates.Zero, new Size(10, 10))),
                Rocket = new("1", "R1"),
                Position = new(-2, -2),
            },
            new Exception("some coordinates error"),
            null,
            new Exception("some coordinates error"),
        };

        yield return new object?[]
        {
            new LandRocketRequest
            {
                Area = new("1", "Area 51", Boundary.From(Coordinates.Zero, new Size(10, 10))),
                Rocket = new("1", "R1"),
                Position = new(2, 2),
            },
            null,
            null,
            Consts.Errors.OutOfPlatform(
                nameof(LandRocketRequest),
                nameof(LandRocketRequest.Position),
                paramName),
        };

        var area = new LandingArea("1", "Area 51", Boundary.From(Coordinates.Zero, new Size(10, 10)));
        var platform = new LandingPlatform("1", "platform 1", new Size(3, 5));
        var rocket = new Rocket("1", "R1");
        var position = new Coordinates(2, 2);

        area.AssignPlatform(platform, Boundary.From(Coordinates.Zero, platform.Size));
        area.LandRocket(new Rocket("2", "other rocket"), platform, new(1, 1), new Coordinates[]
        {
            new(2,2)
        });

        yield return new object?[]
        {
            new LandRocketRequest
            {
                Area = area,
                Rocket = rocket,
                Position = position,
            },
            null,
            null,
            Consts.Errors.Clashes(
                nameof(LandRocketRequest),
                nameof(LandRocketRequest.Position),
                paramName),
        };

        area = new LandingArea("1", "Area 51", Boundary.From(Coordinates.Zero, new Size(10, 10)));
        platform = new LandingPlatform("1", "platform 1", new Size(3, 5));
        rocket = new Rocket("1", "R1");
        position = new Coordinates(2, 2);

        area.AssignPlatform(platform, Boundary.From(Coordinates.Zero, platform.Size));
        area.LandRocket(new Rocket("2", "other rocket"), platform, new(1, 1), Array.Empty<Coordinates>());

        yield return new object?[]
        {
            new LandRocketRequest
            {
                Area = area,
                Rocket = rocket,
                Position = position,
            },
            null,
            new Coordinates[]
            {
                new(1, 1),
                new(1, 2),
                new(1, 3),
                new(2, 1),
                new(2, 1),
                new(3, 1),
                new(3, 2),
                new(3, 3),
            },
            Consts.Errors.Clashes(
                nameof(LandRocketRequest),
                nameof(LandRocketRequest.Position),
                paramName),
        };

        area = new LandingArea("1", "Area 51", Boundary.From(Coordinates.Zero, new Size(10, 10)));
        platform = new LandingPlatform("1", "platform 1", new Size(3, 5));
        rocket = new Rocket("1", "R1");
        position = new Coordinates(2, 4);

        area.AssignPlatform(platform, Boundary.From(Coordinates.Zero, platform.Size));
        area.LandRocket(new Rocket("2", "other rocket"), platform, new(0, 0), Array.Empty<Coordinates>());

        yield return new object?[]
        {
            new LandRocketRequest
            {
                Area = area,
                Rocket = rocket,
                Position = position,
            },
            null,
            new Coordinates[]
            {
                new(1, 1),
                new(1, 2),
                new(1, 3),
                new(2, 1),
                new(2, 1),
                new(3, 1),
                new(3, 2),
                new(3, 3),
            },
            null,
        };
    }
}
