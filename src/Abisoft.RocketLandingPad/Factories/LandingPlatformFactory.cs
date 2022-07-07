using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Abstractions.Providers;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Factories;

internal class LandingPlatformFactory : ILandingPlatformFactory
{
    private readonly ISequenceIdProvider _sequenceIdProvider;

    public LandingPlatformFactory(ISequenceIdProvider sequenceIdProvider)
    {
        _sequenceIdProvider = sequenceIdProvider;
    }

    public LandingPlatform Create(string name, Size size)
    {
        var id = _sequenceIdProvider.GetNextId();

        return new(id, name, size);
    }
}
