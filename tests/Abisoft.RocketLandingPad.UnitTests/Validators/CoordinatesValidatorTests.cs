using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Validators;

namespace Abisoft.RocketLandingPad.UnitTests.Validators;

[Trait("Category", "Unit")]
public class CoordinatesValidatorTests
{
    private readonly CoordinatesValidator _subject;

    public CoordinatesValidatorTests()
    {
        _subject = new();
    }

    [Theory]
    [MemberData(nameof(GetValidateTestData))]
    public void Validate_WhenData_ThenExpectedResult(Coordinates? coordinates, Exception? expectedResult)
    {
        // Act
        var result = _subject.Validate(coordinates);

        //
        result.Should().BeEquivalentTo(expectedResult);
    }

    public static IEnumerable<object?[]> GetValidateTestData()
    {
        const string paramName = "coordinates";

        yield return new object?[]
        {
            null,
            Consts.Errors.CanNotBeNull(nameof(Coordinates), paramName),
        };

        yield return new object?[]
        {
            new Coordinates(-1, 0),
            Consts.Errors.CanNotBeLowerThanZero(nameof(Coordinates), nameof(Coordinates.Row), paramName),
        };

        yield return new object?[]
        {
            new Coordinates(0, -1),
            Consts.Errors.CanNotBeLowerThanZero(nameof(Coordinates), nameof(Coordinates.Column), paramName),
        };

        yield return new object?[]
        {
            new Coordinates(0, 0),
            null,
        };
    }
}
