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
        _sequenceIdProviderMock.VerifyAll();

        GC.SuppressFinalize(this);
    }

    [Fact]
    public void Create_WhenData_ThenCreatesArea()
    {
        // Arrange
        const string expectedId = "1";
        const string expectedName = "Tesla rocket";

        var expectedSize = new Size(3, 2);
        var expectedBoundries = new Boundary(new(0, 0), new(0, 1), new(2, 0), new(2, 1));
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
