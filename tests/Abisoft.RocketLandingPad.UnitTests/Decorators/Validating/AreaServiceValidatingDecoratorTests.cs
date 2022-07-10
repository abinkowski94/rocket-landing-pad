using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Decorators.Validating;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.UnitTests.Decorators.Validating;

[Trait("Category", "Unit")]
public class AreaServiceValidatingDecoratorTests : IDisposable
{
    private readonly Mock<IValidator<Size>> _sizeValidatorMock;
    private readonly Mock<IValidator<AssignPlatformRequest>> _assingRequestValidatorMock;
    private readonly Mock<IValidator<UnassignPlatformRequest>> _unassignRequestValidatorMock;
    private readonly Mock<ILandingAreaService> _decoratedServiceMock;

    private readonly AreaServiceValidatingDecorator _subject;

    public AreaServiceValidatingDecoratorTests()
    {
        _sizeValidatorMock = new(MockBehavior.Strict);
        _assingRequestValidatorMock = new(MockBehavior.Strict);
        _unassignRequestValidatorMock = new(MockBehavior.Strict);
        _decoratedServiceMock = new(MockBehavior.Strict);

        _subject = new(
            _sizeValidatorMock.Object,
            _assingRequestValidatorMock.Object,
            _unassignRequestValidatorMock.Object,
            _decoratedServiceMock.Object);
    }

    public void Dispose()
    {
        _sizeValidatorMock.VerifyAll();
        _assingRequestValidatorMock.VerifyAll();
        _unassignRequestValidatorMock.VerifyAll();
        _decoratedServiceMock.VerifyAll();

        GC.SuppressFinalize(this);
    }

    [Fact]
    public void Create_WhenData_ThenValidatesAndCallsDecoratedService()
    {
        // Arrange
        const string name = "Area 51";
        var expectedSize = new Size(1, 1);
        Result<LandingArea> expectedResult = new LandingArea("1", name, Boundary.From(Coordinates.Zero, expectedSize));

        _sizeValidatorMock
            .Setup(s => s.Validate(expectedSize))
            .Returns<Exception>(null);

        _decoratedServiceMock
            .Setup(s => s.Create(name, expectedSize))
            .Returns(expectedResult);

        // Act
        var result = _subject.Create(name, expectedSize);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void Create_WhenValidationReturnsError_ThenReturnsError()
    {
        // Arrange
        const string name = "Area 51";
        var expectedSize = new Size(1, 1);
        Result<LandingArea> expectedResult = new Exception("some error");

        _sizeValidatorMock
            .Setup(s => s.Validate(expectedSize))
            .Returns(expectedResult.Error);

        // Act
        var result = _subject.Create(name, expectedSize);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void AssignLandingPlatform_WhenData_ThenValidatesAndCallsDecoratedService()
    {
        // Arrange
        var request = new AssignPlatformRequest();

        _assingRequestValidatorMock
            .Setup(s => s.Validate(request))
            .Returns<Exception>(null);

        _decoratedServiceMock
            .Setup(s => s.AssignLandingPlatform(request))
            .Returns(Result.Success);

        // Act
        var result = _subject.AssignLandingPlatform(request);

        // Assert
        result.Should().BeEquivalentTo(Result.Success);
    }

    [Fact]
    public void UnassignLandingPlatform_WhenData_ThenValidatesAndCallsDecoratedService()
    {
        // Arrange
        var request = new UnassignPlatformRequest();

        _unassignRequestValidatorMock
            .Setup(s => s.Validate(request))
            .Returns<Exception>(null);

        _decoratedServiceMock
            .Setup(s => s.UnassignLandingPlatform(request))
            .Returns(Result.Success);

        // Act
        var result = _subject.UnassignLandingPlatform(request);

        // Assert
        result.Should().BeEquivalentTo(Result.Success);
    }
}
