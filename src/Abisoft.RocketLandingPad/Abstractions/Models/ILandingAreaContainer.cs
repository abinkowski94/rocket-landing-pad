using Abisoft.RocketLandingPad.Models.Entities;

namespace Abisoft.RocketLandingPad.Abstractions.Models;

internal interface ILandingAreaContainer
{
    LandingArea? Area { get; }
}
