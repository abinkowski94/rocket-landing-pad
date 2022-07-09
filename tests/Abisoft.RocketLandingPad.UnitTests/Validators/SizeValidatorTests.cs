using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Validators;

namespace Abisoft.RocketLandingPad.UnitTests.Validators;

[Trait("Category", "Unit")]
public class SizeValidatorTests
{
    private readonly SizeValidator _subject;

    public SizeValidatorTests()
    {
        _subject = new();
    }

    [Theory]
    [MemberData(nameof(GetValidateTestData))]
    public void Validate_WhenData_ThenExpectedResult(Size? size, Exception? expectedResult)
    {
        // Act
        var result = _subject.Validate(size);

        //
        result.Should().BeEquivalentTo(expectedResult);
    }

    public static IEnumerable<object?[]> GetValidateTestData()
    {
        const string paramName = "size";

        yield return new object?[]
        {
            null,
            Consts.Errors.CanNotBeNull(nameof(Size), paramName),
        };

        yield return new object?[]
        {
            new Size(0, 1),
            Consts.Errors.CanNotBeLowerThanOne(nameof(Size), nameof(Size.Height), paramName),
        };

        yield return new object?[]
        {
            new Size(1, 0),
            Consts.Errors.CanNotBeLowerThanOne(nameof(Size), nameof(Size.Width), paramName),
        };

        yield return new object?[]
        {
            new Size(1, 1),
            null,
        };
    }
}
