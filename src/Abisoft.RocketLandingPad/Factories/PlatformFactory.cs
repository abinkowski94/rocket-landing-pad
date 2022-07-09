using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Abstractions.Providers;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Factories;

internal class PlatformFactory : IPlatformFactory
{
    private readonly ISequenceIdProvider _sequenceIdProvider;

    public PlatformFactory(ISequenceIdProvider sequenceIdProvider)
    {
        _sequenceIdProvider = sequenceIdProvider;
    }

    public LandingPlatform Create(string name, Size size)
    {
        var id = _sequenceIdProvider.GetNextId();

        return new(id, name, size);
    }
}
