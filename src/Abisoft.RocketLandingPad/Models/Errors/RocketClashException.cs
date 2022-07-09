namespace Abisoft.RocketLandingPad.Models.Errors;

public class RocketClashException : ArgumentException
{
    public RocketClashException() : base()
    {
    }

    public RocketClashException(string? message) : base(message)
    {
    }

    public RocketClashException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public RocketClashException(string? message, string? paramName) : base(message, paramName)
    {
    }

    public RocketClashException(string? message, string? paramName, Exception? innerException) : base(message, paramName, innerException)
    {
    }
}
