using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Abstractions.Providers;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.PositioningComponents;

namespace Abisoft.RocketLandingPad.Factories;

internal class LandingAreaFactory : ILandingAreaFactory
{
    private readonly ISequenceIdProvider _sequenceIdProvider;
    private readonly ICoordinatesFactory _coordinatesFactory;

    public LandingAreaFactory(
        ISequenceIdProvider sequenceIdProvider,
        ICoordinatesFactory coordinatesFactory)
    {
        _sequenceIdProvider = sequenceIdProvider;
        _coordinatesFactory = coordinatesFactory;
    }

    public LandingArea Create(string name, Size size)
    {
        var id = _sequenceIdProvider.GetNextId();
        var boundries = _coordinatesFactory.CreateBoundary(topLeft: Coordinates.Zero, size);

        return new(id, name, boundries);
    }
}
