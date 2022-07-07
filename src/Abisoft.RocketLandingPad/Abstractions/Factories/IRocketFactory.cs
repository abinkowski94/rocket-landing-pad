using Abisoft.RocketLandingPad.Models.Entities;

namespace Abisoft.RocketLandingPad.Abstractions.Factories;

internal interface IRocketFactory
{
    Rocket Create(string name);
}
