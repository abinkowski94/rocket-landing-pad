using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Abstractions.Validators;
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
    public void Validate_WhenData_ThenExpectedResult(LandRocketRequest? request, Exception? expectedResult)
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
            Consts.Errors.CanNotBeNull(nameof(LandRocketRequest), paramName),
        };

        yield return new object?[]
        {
            new LandRocketRequest(),
            Consts.Errors.CanNotBeNull(nameof(LandRocketRequest), nameof(LandRocketRequest.Area), paramName),
        };
    }
}
