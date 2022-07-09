using Abisoft.RocketLandingPad.Abstractions.Providers;
using Abisoft.RocketLandingPad.Factories;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.UnitTests.Factories;

[Trait("Category", "Unit")]
public class PlatformFactoryTests : IDisposable
{
    private readonly Mock<ISequenceIdProvider> _sequenceIdProviderMock;
    private readonly PlatformFactory _subject;

    public PlatformFactoryTests()
    {
        _sequenceIdProviderMock = new(MockBehavior.Strict);
        _subject = new(_sequenceIdProviderMock.Object);
    }

    public void Dispose()
    {
        _sequenceIdProviderMock.VerifyAll();

        GC.SuppressFinalize(this);
    }

    [Fact]
    public void Create_WhenData_ThenCreatesPlatform()
    {
        // Arrange
        const string expectedId = "1";
        const string expectedName = "Tesla platform";

        var expectedSize = new Size(3, 2);
        var expectedResult = new LandingPlatform(expectedId, expectedName, expectedSize);

        _sequenceIdProviderMock
            .Setup(p => p.GetNextId())
            .Returns(expectedId);

        // Act
        var result = _subject.Create(expectedName, expectedSize);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}
