using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.Results;
using Abisoft.RocketLandingPad.Services;

namespace Abisoft.RocketLandingPad.UnitTests.Services;

[Trait("Category", "Unit")]
public class RocketServiceTests : IDisposable
{
    private readonly Mock<IRocketFactory> _rocketFactoryMock;
    private readonly RocketService _subject;

    public RocketServiceTests()
    {
        _rocketFactoryMock = new(MockBehavior.Strict);
        _subject = new(_rocketFactoryMock.Object);
    }

    public void Dispose()
    {
        _rocketFactoryMock.VerifyAll();

        GC.SuppressFinalize(this);
    }

    [Fact]
    public void Create_WhenData_ThenCallsFactory()
    {
        // Arrange
        const string expectedName = "Rocket one";
        Result<Rocket> expectedResult = new Rocket("1", expectedName);

        _rocketFactoryMock
            .Setup(f => f.Create(expectedName))
            .Returns(expectedResult.Value!);

        // Act
        var result = _subject.Create(expectedName);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }
}
