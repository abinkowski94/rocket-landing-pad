namespace Abisoft.RocketLandingPad.Models.Results;

public struct Result<T>
{
    public T? Value { get; }

    public Exception? Error { get; }

    public bool IsError => Error is not null;

    public bool IsSuccess => !IsError;

    public Result(T value)
    {
        Value = value;
        Error = default;
    }

    public Result(Exception error)
    {
        Value = default;
        Error = error;
    }

    public static implicit operator T?(Result<T> result)
    {
        return result.Value;
    }

    public static implicit operator Result<T>(T value)
    {
        return new Result<T>(value);
    }

    public static implicit operator Result<T>(Exception error)
    {
        return new Result<T>(error);
    }

    public static implicit operator Result(Result<T> value)
    {
        if (value.IsError)
        {
            return value.Error!;
        }

        return Result.Success;
    }
}
