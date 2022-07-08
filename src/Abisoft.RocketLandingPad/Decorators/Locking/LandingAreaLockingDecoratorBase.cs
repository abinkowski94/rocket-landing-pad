using System.Runtime.CompilerServices;
using Abisoft.RocketLandingPad.Abstractions.Models;
using Abisoft.RocketLandingPad.Models.Requests;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Decorators.Locking;

internal class LandingAreaLockingDecoratorBase
{
    protected static Result ExecuteWithSyncLock<T>(
        T request,
        Func<T, Result> action,
        [CallerArgumentExpression("request")] string? paramName = default)
            where T : ILandingAreaContainer
    {
        var nullValidation = ValidateAgainstNullArea(request, paramName);
        if (nullValidation.IsError)
        {
            return nullValidation;
        }

        lock (request.Area!.SyncRoot)
        {
            return action(request);
        }
    }

    private static Result ValidateAgainstNullArea<T>(T request, string? paramName)
        where T : ILandingAreaContainer
    {
        if (request is null)
        {
            return new ArgumentNullException(
                paramName,
                $"{nameof(CanLandRocketRequest)} can not be null.");
        }

        if (request.Area is null)
        {
            return new ArgumentException(
                $"{nameof(CanLandRocketRequest)}.{nameof(CanLandRocketRequest.Area)} can not be null.",
                paramName);
        }

        return new Result();
    }
}
