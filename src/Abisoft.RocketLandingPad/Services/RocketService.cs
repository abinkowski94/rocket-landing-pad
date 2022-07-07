using Abisoft.RocketLandingPad.Abstractions.Factories;
using Abisoft.RocketLandingPad.Abstractions.Services;
using Abisoft.RocketLandingPad.Models.Entities;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Services;

internal class RocketService : IRocketService
{
    private readonly IRocketFactory _rocketFactory;

    public RocketService(IRocketFactory rocketFactory)
    {
        _rocketFactory = rocketFactory;
    }

    public Result<Rocket> Create(string name)
    {
        return _rocketFactory.Create(name);
    }
}
