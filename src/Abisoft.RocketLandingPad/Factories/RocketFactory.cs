using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Abstractions.Providers;
using Abisoft.RocketLandingPad.Models.Entities;

namespace Abisoft.RocketLandingPad.Factories;

internal class RocketFactory : IRocketFactory
{
    private readonly ISequenceIdProvider _sequenceIdProvider;

    public RocketFactory(ISequenceIdProvider sequenceIdProvider)
    {
        _sequenceIdProvider = sequenceIdProvider;
    }

    public Rocket Create(string name)
    {
        var id = _sequenceIdProvider.GetNextId();

        return new Rocket(id, name);
    }
}
