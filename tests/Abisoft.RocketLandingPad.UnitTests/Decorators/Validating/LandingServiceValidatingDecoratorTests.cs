using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Decorators.Validating;
using Abisoft.RocketLandingPad.Models.Errors;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.UnitTests.Decorators.Validating;

[Trait("Category", "Unit")]
public class LandingServiceValidatingDecoratorTests : IDisposable
{
    private readonly Mock<IValidator<LandRocketRequest>> _landRocketRequestValidatorMock;
    private readonly Mock<IValidator<StartRocketRequest>> _startRocketRequestValidatorMock;
    private readonly Mock<ILandingService> _decoratedServiceMock;

    private readonly LandingServiceValidatingDecorator _subject;

    public LandingServiceValidatingDecoratorTests()
    {
        _landRocketRequestValidatorMock = new(MockBehavior.Strict);
        _startRocketRequestValidatorMock = new(MockBehavior.Strict);
        _decoratedServiceMock = new(MockBehavior.Strict);

        _subject = new(
            _landRocketRequestValidatorMock.Object,
            _startRocketRequestValidatorMock.Object,
            _decoratedServiceMock.Object);
    }

    public void Dispose()
    {
        _landRocketRequestValidatorMock.VerifyAll();
        _startRocketRequestValidatorMock.VerifyAll();
        _decoratedServiceMock.VerifyAll();

        GC.SuppressFinalize(this);
    }

    [Theory]
    [MemberData(nameof(GetCanLandRocketInfoTestData))]
    public void CanLandRocketInfo_WhenData_ThenExpectedResult(Exception? error, string expectedResult)
    {
        // Arrange
        var request = new LandRocketRequest();

        _landRocketRequestValidatorMock
            .Setup(s => s.Validate(request))
            .Returns(error);

        if (error is null)
        {
            _decoratedServiceMock
                .Setup(s => s.CanLandRocket(request))
                .Returns(Result.Success);
        }

        // Act
        var result = _subject.CanLandRocketInfo(request);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void CanLandRocket_WhenData_ThenValidatesAndCallsDecoratedService()
    {
        // Arrange
        var request = new LandRocketRequest();

        _landRocketRequestValidatorMock
            .Setup(s => s.Validate(request))
            .Returns<Exception>(null);

        _decoratedServiceMock
            .Setup(s => s.CanLandRocket(request))
            .Returns(Result.Success);

        // Act
        var result = _subject.CanLandRocket(request);

        // Assert
        result.Should().BeEquivalentTo(Result.Success);
    }

    [Fact]
    public void LandRocket_WhenData_ThenValidatesAndCallsDecoratedService()
    {
        // Arrange
        var request = new LandRocketRequest();

        _landRocketRequestValidatorMock
            .Setup(s => s.Validate(request))
            .Returns<Exception>(null);

        _decoratedServiceMock
            .Setup(s => s.LandRocket(request))
            .Returns(Result.Success);

        // Act
        var result = _subject.LandRocket(request);

        // Assert
        result.Should().BeEquivalentTo(Result.Success);
    }

    [Fact]
    public void StartRocket_WhenData_ThenValidatesAndCallsDecoratedService()
    {
        // Arrange
        var request = new StartRocketRequest();

        _startRocketRequestValidatorMock
            .Setup(s => s.Validate(request))
            .Returns<Exception>(null);

        _decoratedServiceMock
            .Setup(s => s.StartRocket(request))
            .Returns(Result.Success);

        // Act
        var result = _subject.StartRocket(request);

        // Assert
        result.Should().BeEquivalentTo(Result.Success);
    }

    public static IEnumerable<object?[]> GetCanLandRocketInfoTestData()
    {
        yield return new object?[]
        {
            null,
            Consts.LandingStates.OkForLanding,
        };

        yield return new object?[]
        {
            new RocketOutOfPlatformException(),
            Consts.LandingStates.OutOfPlatform,
        };

        yield return new object?[]
        {
            new RocketClashException(),
            Consts.LandingStates.Clash,
        };
    }
}
