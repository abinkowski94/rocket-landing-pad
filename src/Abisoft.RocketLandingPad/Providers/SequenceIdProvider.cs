using Abisoft.RocketLandingPad.Abstractions.Providers;

namespace Abisoft.RocketLandingPad.Providers;

internal class SequenceIdProvider : ISequenceIdProvider
{
    private int _currentId = 0;

    public string GetNextId()
    {
        var result = Interlocked.Increment(ref _currentId);

        return result.ToString();
    }
}
