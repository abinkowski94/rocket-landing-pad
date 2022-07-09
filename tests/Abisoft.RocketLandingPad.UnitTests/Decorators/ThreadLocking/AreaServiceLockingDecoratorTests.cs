using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Decorators.ThreadLocking;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.UnitTests.Decorators.ThreadLocking;

[Trait("Category", "Unit")]
public class AreaServiceLockingDecoratorTests : IDisposable
{
    private readonly Mock<ILandingAreaService> _decoratedServiceMock;
    private readonly AreaServiceLockingDecorator _subject;

    public AreaServiceLockingDecoratorTests()
    {
        _decoratedServiceMock = new(MockBehavior.Strict);
        _subject = new(_decoratedServiceMock.Object);
    }

    public void Dispose()
    {
        _decoratedServiceMock.VerifyAll();

        GC.SuppressFinalize(this);
    }

    [Fact]
    public void Create_WhenData_ThenCallsDecoratedService()
    {
        // Arrange
        const string name = "Area 51";
        var expectedSize = new Size(1, 1);
        Result<LandingArea> expectedResult = new LandingArea("1", name, Boundary.From(Coordinates.Zero, expectedSize));

        _decoratedServiceMock
            .Setup(s => s.Create(name, expectedSize))
            .Returns(expectedResult);

        // Act
        var result = _subject.Create(name, expectedSize);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void AssignLandingPlatform_WhenData_ThenCallsDecoratedService()
    {
        // Arrange
        var request = new AssignPlatformRequest
        {
            Area = new LandingArea("1", "Test", Boundary.From(Coordinates.Zero, new(1, 1))),
        };

        _decoratedServiceMock
            .Setup(s => s.AssignLandingPlatform(request))
            .Returns(new Result());

        // Act
        _subject.AssignLandingPlatform(request);
    }

    [Fact]
    public void UnassignLandingPlatform_WhenData_ThenCallsDecoratedService()
    {
        // Arrange
        var request = new UnassignPlatformRequest
        {
            Area = new LandingArea("1", "Test", Boundary.From(Coordinates.Zero, new(1, 1))),
        };

        _decoratedServiceMock
            .Setup(s => s.UnassignLandingPlatform(request))
            .Returns(new Result());

        // Act
        _subject.UnassignLandingPlatform(request);
    }

    [Fact]
    public void UnassignLandingPlatform_WhenRequestIsNull_ThenReturnsErrorResult()
    {
        // Arrange
        Result expectedResult = Consts.Errors.CanNotBeNull(
            nameof(UnassignPlatformRequest),
            "request");

        // Act
        var result = _subject.UnassignLandingPlatform(null!);

        // Assert
        result.Error.Should().BeEquivalentTo(expectedResult.Error);
    }

    [Fact]
    public void UnassignLandingPlatform_WhenAreaIsNull_ThenReturnsErrorResult()
    {
        // Arrange
        var request = new UnassignPlatformRequest();

        Result expectedResult = Consts.Errors.CanNotBeNull(
            nameof(UnassignPlatformRequest),
            nameof(UnassignPlatformRequest.Area),
            "request");

        // Act
        var result = _subject.UnassignLandingPlatform(request);

        // Assert
        result.Error.Should().BeEquivalentTo(expectedResult.Error);
    }
}
