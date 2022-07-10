using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;
using Abisoft.RocketLandingPad.Models.Results;
using Abisoft.RocketLandingPad.Services;

namespace Abisoft.RocketLandingPad.UnitTests.Services;

[Trait("Category", "Unit")]
public class PlatformServiceTests : IDisposable
{
    private readonly Mock<IPlatformFactory> _platformFactoryMock;
    private readonly PlatformService _subject;

    public PlatformServiceTests()
    {
        _platformFactoryMock = new(MockBehavior.Strict);
        _subject = new(_platformFactoryMock.Object);
    }

    public void Dispose()
    {
        _platformFactoryMock.VerifyAll();

        GC.SuppressFinalize(this);
    }

    [Fact]
    public void Create_WhenData_ThenCallsFactory()
    {
        // Arrange
        const string expectedName = "Platfrom one";
        var expectedSize = new Size(2, 2);
        Result<LandingPlatform> expectedResult = new LandingPlatform("1", expectedName, expectedSize);

        _platformFactoryMock
            .Setup(f => f.Create(expectedName, expectedSize))
            .Returns(expectedResult.Value!);

        // Act
        var result = _subject.Create(expectedName, expectedSize);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}
