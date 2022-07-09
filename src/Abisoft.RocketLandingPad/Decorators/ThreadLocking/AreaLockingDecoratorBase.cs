using System.Runtime.CompilerServices;
using Abisoft.RocketLandingPad.Abstractions.Models;
using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.Decorators.ThreadLocking;

internal abstract class AreaLockingDecoratorBase
{
    protected static Result ExecuteWithSyncLock<T>(
        T request,
        Func<T, Result> action,
        [CallerArgumentExpression("request")] string? paramName = default)
            where T : ILandingAreaContainer
    {
        var nullValidation = ValidateAgainstNullArea(request, paramName!);
        if (nullValidation.IsError)
        {
            return nullValidation;
        }

        lock (request.Area!.SyncRoot)
        {
            return action(request);
        }
    }

    private static Result ValidateAgainstNullArea<T>(T request, string paramName)
        where T : ILandingAreaContainer
    {
        if (request is null)
        {
            return Consts.Errors.CanNotBeNull(
                typeof(T).Name,
                paramName);
        }

        if (request.Area is null)
        {
            return Consts.Errors.CanNotBeNull(
                typeof(T).Name,
                nameof(ILandingAreaContainer.Area),
                paramName);
        }

        return new Result();
    }
}
