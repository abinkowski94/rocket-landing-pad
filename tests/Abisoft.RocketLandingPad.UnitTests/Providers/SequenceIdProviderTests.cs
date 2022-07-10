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
        const int tasksCount = 10_000;

        var expectedResult = Enumerable.Range(1, tasksCount)
            .Select(e => e.ToString())
            .ToList();

        var tasks = Enumerable.Range(1, tasksCount)
            .Select(_ => Task.Run(() => _subject.GetNextId()))
            .ToList();

        // Act
        var result = await Task.WhenAll(tasks);
        result = result.OrderBy(e => int.Parse(e)).ToArray();

        // Assert
        Assert.Equal(expectedResult, result);
    }
}
