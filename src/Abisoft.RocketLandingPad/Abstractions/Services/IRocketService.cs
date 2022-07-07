using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Abstractions.Services;

public interface IRocketService
{
    Result<Rocket> Create(string name);
}
