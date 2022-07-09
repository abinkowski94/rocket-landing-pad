using Abisoft.RocketLandingPad.Models.Errors;

namespace Abisoft.RocketLandingPad;

internal static class Consts
{
    internal static class LandingStates
    {
        internal const string OkForLanding = "ok for landing";
        internal const string OutOfPlatform = "out of platform";
        internal const string Clash = "clash";
    }

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

        internal static ArgumentException MustBeGrounded(string typeName, string propertyName, string paramName)
            => new(Messages.MustBeGrounded(typeName, propertyName), paramName);

        internal static ArgumentException AlreadyContainsPlatform(string typeName, string propertyName, string platformId, string paramName)
            => new(Messages.AlreadyContainsPlatform(typeName, propertyName, platformId), paramName);

        internal static ArgumentException DoesNotContainPlatform(string typeName, string propertyName, string platformId, string paramName)
            => new(Messages.DoesNotContainPlatform(typeName, propertyName, platformId), paramName);

        internal static ArgumentException PlatformAlreadyUnassigned(string typeName, string propertyName, string paramName)
            => new(Messages.PlatformAlreadyUnassigned(typeName, propertyName), paramName);

        internal static RocketOutOfPlatformException OutOfPlatform(string typeName, string propertyName, string paramName)
            => new(Messages.OutOfPlatform(typeName, propertyName), paramName);

        internal static RocketClashException Clashes(string typeName, string propertyName, string paramName)
            => new(Messages.Clashes(typeName, propertyName), paramName);

        internal static ArgumentException StillContainsRockets(string typeName, string propertyName, string paramName)
            => new(Messages.StillContainsRockets(typeName, propertyName), paramName);

        internal static ArgumentException DoesNotFitInsideArea(string typeName, string propertyName, string platformId, string areaId, string paramName)
            => new(Messages.DoesNotFitInsideArea(typeName, propertyName, platformId, areaId), paramName);

        internal static ArgumentException OverlapsWithExistingPlatform(string typeName, string propertyName, string newPlatformId, string existingPlatformId, string paramName)
            => new(Messages.OverlapsWithExistingPlatform(typeName, propertyName, newPlatformId, existingPlatformId), paramName);

        internal static ArgumentException NotPlacedInsidePlatform(string typeName, string propertyName, string rocketId, string areaId, string paramName)
            => new(Messages.NotPlacedInsidePlatform(typeName, propertyName, rocketId, areaId), paramName);

        internal static ArgumentException DoesNotContainRocket(string typeName, string propertyName, string areaId, string rocketId, string paramName)
            => new(Messages.DoesNotContainRocket(typeName, propertyName, areaId, rocketId), paramName);

        internal static class Messages
        {
            internal static string CanNotBeNull(string typeName)
                => $"{typeName} can not be null.";

            internal static string CanNotBeNull(string typeName, string propertyName)
                => $"{typeName}.{propertyName} can not be null.";

            internal static string MustBeGrounded(string typeName, string propertyName)
                => $"{typeName}.{propertyName} must be grounded.";

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

            internal static string DoesNotFitInsideArea(string typeName, string propertyName, string platformId, string areaId)
                => $"{typeName}.{propertyName}: '{platformId}' does not fit inside area: '{areaId}'.";

            internal static string OverlapsWithExistingPlatform(string typeName, string propertyName, string newPlatformId, string existingPlatformId)
                => $"{typeName}.{propertyName}: '{newPlatformId}' overlpas with existing platform: '{existingPlatformId}'.";

            internal static string NotPlacedInsidePlatform(string typeName, string propertyName, string rocketId, string areaId)
                => $"{typeName}.{propertyName}: '{rocketId}' is not placed inside area: '{areaId}'.";

            internal static string DoesNotContainRocket(string typeName, string propertyName, string areaId, string rocketId)
                => $"{typeName}.{propertyName}: '{areaId}' does not contain rocket: '{rocketId}'.";

            internal static string OutOfPlatform(string typeName, string propertyName)
                => $"{typeName}.{propertyName} is out of platform.";

            internal static string Clashes(string typeName, string propertyName)
                => $"{typeName}.{propertyName} clashes with other rocket.";
        }
    }
}
