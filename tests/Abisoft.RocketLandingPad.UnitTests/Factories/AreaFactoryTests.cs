using Abisoft.RocketLandingPad.Abstractions.Providers;
using Abisoft.RocketLandingPad.Factories;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.UnitTests.Factories;

[Trait("Category", "Unit")]
public class AreaFactoryTests : IDisposable
{
    private readonly Mock<ISequenceIdProvider> _sequenceIdProviderMock;
    private readonly AreaFactory _subject;

    public AreaFactoryTests()
    {
        _sequenceIdProviderMock = new(MockBehavior.Strict);
        _subject = new(_sequenceIdProviderMock.Object);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _sequenceIdProviderMock.VerifyAll();
    }

    [Fact]
    public void Create_WhenData_ThenCreatesArea()
    {
        // Arrange
        const string expectedId = "1";
        const string expectedName = "Tesla rocket";

        var expectedSize = new Size(2, 3);
        var expectedBoundries  = Boundary.From(topLeft: Coordinates.Zero, expectedSize);
        var expectedResult = new LandingArea(expectedId, expectedName, expectedBoundries);

        _sequenceIdProviderMock
            .Setup(p => p.GetNextId())
            .Returns(expectedId);

        // Act
        var result = _subject.Create(expectedName, expectedSize);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}
