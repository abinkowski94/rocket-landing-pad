using System.Runtime.CompilerServices;
using Abisoft.RocketLandingPad.Abstractions.Validators;
using Abisoft.RocketLandingPad.Extensions;

[assembly: InternalsVisibleTo("Abisoft.RocketLandingPad.UnitTests")]
[assembly: InternalsVisibleTo("Abisoft.RocketLandingPad.IntegrationTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace Abisoft.RocketLandingPad.Properties;

public static class AssemblyMarker
{
    private static readonly Lazy<IEnumerable<Type>> _validatorTypes = new(GetValidatorTypes);

    internal static IEnumerable<Type> ValidatorTypes => _validatorTypes.Value;

    private static IEnumerable<Type> GetValidatorTypes()
    {
        return typeof(AssemblyMarker)
             .Assembly
             .DefinedTypes
             .Where(t => t.IsClass && !t.IsAbstract && t.ImplementsGenericInterface(typeof(IValidator<>)));
    }
}
