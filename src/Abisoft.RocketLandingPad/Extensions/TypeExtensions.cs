namespace Abisoft.RocketLandingPad.Extensions;

internal static class TypeExtensions
{
    internal static bool ImplementsGenericInterface(this Type type, Type genericInterfaceType)
    {
        return type.GetInterfaces()
            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericInterfaceType);
    }
}
