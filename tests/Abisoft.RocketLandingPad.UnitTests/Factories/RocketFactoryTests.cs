using Abisoft.RocketLandingPad.Abstractions.Providers;
using Abisoft.RocketLandingPad.Factories;
using Abisoft.RocketLandingPad.Models.Entities;

namespace Abisoft.RocketLandingPad.UnitTests.Factories;

[Trait("Category", "Unit")]
public class RocketFactoryTests : IDisposable
{
    private readonly Mock<ISequenceIdProvider> _sequenceIdProviderMock;
    private readonly RocketFactory _subject;

    public RocketFactoryTests()
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
    public void Create_WhenData_ThenCreatesRocket()
    {
        // Arrange
        const string expectedId = "1";
        const string expectedName = "Tesla rocket";

        var expectedResult = new Rocket(expectedId, expectedName);

        _sequenceIdProviderMock
            .Setup(p => p.GetNextId())
            .Returns(expectedId);

        // Act
        var result = _subject.Create(expectedName);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}
