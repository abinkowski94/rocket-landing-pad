using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Abstractions.Providers;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Factories;

internal class AreaFactory : IAreaFactory
{
    private readonly ISequenceIdProvider _sequenceIdProvider;

    public AreaFactory(
        ISequenceIdProvider sequenceIdProvider)
    {
        _sequenceIdProvider = sequenceIdProvider;
    }

    public LandingArea Create(string name, Size size)
    {
        var id = _sequenceIdProvider.GetNextId();
        var boundries = Boundary.From(topLeft: Coordinates.Zero, size);

        return new(id, name, boundries);
    }
}
