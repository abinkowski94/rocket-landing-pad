using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Decorators.Validating;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.UnitTests.Decorators.Validating;

[Trait("Category", "Unit")]
public class PlatformServiceValidatingDecoratorTests : IDisposable
{
    private readonly Mock<IValidator<Size>> _sizeValidatorMock;
    private readonly Mock<ILandingPlatformService> _decoratedServiceMock;

    private readonly PlatformServiceValidatingDecorator _subject;

    public PlatformServiceValidatingDecoratorTests()
    {
        _sizeValidatorMock = new(MockBehavior.Strict);
        _decoratedServiceMock = new(MockBehavior.Strict);

        _subject = new(
            _sizeValidatorMock.Object,
            _decoratedServiceMock.Object);
    }

    public void Dispose()
    {
        _sizeValidatorMock.VerifyAll();
        _decoratedServiceMock.VerifyAll();

        GC.SuppressFinalize(this);
    }

    [Fact]
    public void Create_WhenData_ThenValidatesAndCallsDecoratedService()
    {
        // Arrange
        const string name = "Tesla platform";
        var expectedSize = new Size(10, 10);
        Result<LandingPlatform> expectedResult = new LandingPlatform("1", name, expectedSize);

        _sizeValidatorMock
            .Setup(v => v.Validate(expectedSize))
            .Returns<Exception?>(null);

        _decoratedServiceMock
            .Setup(s => s.Create(name, expectedSize))
            .Returns(expectedResult);

        // Act
        var result = _subject.Create(name, expectedSize);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void Create_WhenInvalidData_ThenValidatesAndReturnsError()
    {
        // Arrange
        const string name = "Tesla platform";
        var expectedSize = new Size(10, 10);
        Result<LandingPlatform> expectedResult = new Exception("Test error");

        _sizeValidatorMock
            .Setup(v => v.Validate(expectedSize))
            .Returns(expectedResult.Error);

        // Act
        var result = _subject.Create(name, expectedSize);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}
