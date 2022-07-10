using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Decorators.ThreadLocking;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.UnitTests.Decorators.ThreadLocking;

[Trait("Category", "Unit")]
public class LandingServiceLockingDecoratorTests : IDisposable
{
    private readonly Mock<ILandingService> _decoratedServiceMock;
    private readonly LandingServiceLockingDecorator _subject;

    public LandingServiceLockingDecoratorTests()
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
    public void CanLandRocketInfo_WhenData_ThenCallsDecoratedService()
    {
        // Arrange
        var request = new LandRocketRequest
        {
            Area = new LandingArea("1", "Test", Boundary.From(Coordinates.Zero, new(1, 1))),
        };

        _decoratedServiceMock
            .Setup(s => s.CanLandRocketInfo(request))
            .Returns("empty");

        // Act
        _subject.CanLandRocketInfo(request);
    }

    [Fact]
    public void CanLandRocket_WhenData_ThenCallsDecoratedService()
    {
        // Arrange
        var request = new LandRocketRequest
        {
            Area = new LandingArea("1", "Test", Boundary.From(Coordinates.Zero, new(1, 1))),
        };

        _decoratedServiceMock
            .Setup(s => s.CanLandRocket(request))
            .Returns(new Result());

        // Act
        _subject.CanLandRocket(request);
    }

    [Fact]
    public void LandRocket_WhenData_ThenCallsDecoratedService()
    {
        // Arrange
        var request = new LandRocketRequest
        {
            Area = new LandingArea("1", "Test", Boundary.From(Coordinates.Zero, new(1, 1))),
        };

        _decoratedServiceMock
            .Setup(s => s.LandRocket(request))
            .Returns(new Result());

        // Act
        _subject.LandRocket(request);
    }

    [Fact]
    public void StartRocket_WhenData_ThenCallsDecoratedService()
    {
        // Arrange
        var request = new StartRocketRequest
        {
            Area = new LandingArea("1", "Test", Boundary.From(Coordinates.Zero, new(1, 1))),
        };

        _decoratedServiceMock
            .Setup(s => s.StartRocket(request))
            .Returns(new Result());

        // Act
        _subject.StartRocket(request);
    }

    [Fact]
    public async void StartRocket_WhenMultipleThreadsThen_ThenCallsDecoratedServiceAndDoNotOverlaps()
    {
        // Arrange
        const int expectedEnumerations = 10_000;

        var processed = 0;
        var request = new StartRocketRequest
        {
            Area = new LandingArea("1", "Test", Boundary.From(Coordinates.Zero, new(1, 1))),
        };

        _decoratedServiceMock
            .Setup(s => s.StartRocket(request))
            .Returns(new Result())
            .Callback(() => processed++);

        var tasks = Enumerable.Range(1, expectedEnumerations)
            .Select(_ => Task.Run(() => _subject.StartRocket(request)))
            .ToList();

        // Act
        await Task.WhenAll(tasks);

        // Assert
        processed.Should().Be(expectedEnumerations);
    }
}
