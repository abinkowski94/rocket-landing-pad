namespace Abisoft.RocketLandingPad.Models.Results;

public struct Result
{
    public Exception? Error { get; }

    public bool IsSuccess => Error is null;

    public bool IsError => Error is not null;

    public Result()
    {
        Error = default;
    }

    public Result(Exception error)
    {
        Error = error;
    }

    public static implicit operator Result(Exception error)
    {
        return new Result(error);
    }
}
