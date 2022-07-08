namespace Abisoft.RocketLandingPad;

internal static class Consts
{
    internal static class Errors
    {
        internal static ArgumentNullException CanNotBeNull(string typeName, string paramName)
            => new(paramName, Messages.CanNotBeNull(typeName));

        internal static ArgumentException CanNotBeNull(string typeName, string propertyName, string paramName)
            => new(Messages.CanNotBeNull(typeName, propertyName), paramName);

        internal static ArgumentException CanNotBeLowerThanZero(string typeName, string propertyName, string paramName)
            => new(Messages.CanNotBeLowerThanZero(typeName, propertyName), paramName);

        internal static ArgumentException CanNotBeLowerThanOne(string typeName, string propertyName, string paramName)
            => new(Messages.CanNotBeLowerThanOne(typeName, propertyName), paramName);

        internal static ArgumentException AlreadyContainsPlatform(string typeName, string propertyName, string platformId, string paramName)
            => new(Messages.AlreadyContainsPlatform(typeName, propertyName, platformId), paramName);

        internal static ArgumentException DoesNotContainPlatform(string typeName, string propertyName, string platformId, string paramName)
            => new(Messages.DoesNotContainPlatform(typeName, propertyName, platformId), paramName);

        internal static ArgumentException PlatformAlreadyUnassigned(string typeName, string propertyName, string paramName)
            => new(Messages.PlatformAlreadyUnassigned(typeName, propertyName), paramName);

        internal static ArgumentException StillContainsRockets(string typeName, string propertyName, string paramName)
            => new(Messages.StillContainsRockets(typeName, propertyName), paramName);

        internal static class Messages
        {
            internal static string CanNotBeNull(string typeName)
                => $"{typeName} can not be null.";

            internal static string CanNotBeNull(string typeName, string propertyName)
                => $"{typeName}.{propertyName} can not be null.";

            internal static string CanNotBeLowerThanZero(string typeName, string propertyName)
                => $"{typeName}.{propertyName} can not be lower than zero.";

            internal static string CanNotBeLowerThanOne(string typeName, string propertyName)
                => $"{typeName}.{propertyName} can not be lower than one.";

            internal static string AlreadyContainsPlatform(string typeName, string propertyName, string platformId)
                => $"{typeName}.{propertyName} already contains platform: '{platformId}'.";

            internal static string DoesNotContainPlatform(string typeName, string propertyName, string platformId)
                => $"{typeName}.{propertyName} does not contain platform: '{platformId}'.";

            internal static string PlatformAlreadyUnassigned(string typeName, string propertyName)
                => $"{typeName}.{propertyName} is already unassigned.";

            internal static string StillContainsRockets(string typeName, string propertyName)
                => $"{typeName}.{propertyName} still contains rockets.";
        }
    }
}
