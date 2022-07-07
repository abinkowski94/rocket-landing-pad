using Abisoft.RocketLandingPad.Models.Entities;

namespace Abisoft.RocketLandingPad.Abstractions.Serializers;

public interface ILandingAreaTextSerializer
{
    string Serialize(LandingArea landingArea);
}
