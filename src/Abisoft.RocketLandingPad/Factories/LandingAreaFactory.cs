using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Abstractions.Providers;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Factories;

internal class LandingAreaFactory : ILandingAreaFactory
{
    private readonly ISequenceIdProvider _sequenceIdProvider;

    public LandingAreaFactory(ISequenceIdProvider sequenceIdProvider)
    {
        _sequenceIdProvider = sequenceIdProvider;
    }

    public LandingArea Create(string name, Size size)
    {
        var id = _sequenceIdProvider.GetNextId();

        return new(id, name, size);
    }
}
