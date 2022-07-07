using Abisoft.RocketLandingPad.Providers;

namespace Abisoft.RocketLandingPad.UnitTests.Providers;

[Trait("Category", "Unit")]
public class SequenceIdProviderTests
{
    private readonly SequenceIdProvider _subject;

    public SequenceIdProviderTests()
    {
        _subject = new();
    }

    [Fact]
    public async Task GetNextId_WhenThereAreMultipleThreads_ThenSequenceIsKept()
    {
        // Arrange
        var expectedResult = Enumerable.Range(1, 1_000_000)
            .Select(e => e.ToString())
            .ToList();

        var tasks = Enumerable.Range(1, 1_000_000)
            .Select(_ => Task.Run(() => _subject.GetNextId()))
            .ToList();

        // Act
        var result = await Task.WhenAll(tasks);
        result = result.OrderBy(e => int.Parse(e)).ToArray();

        // Assert
        Assert.Equal(expectedResult, result);
    }
}
