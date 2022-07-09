namespace Abisoft.RocketLandingPad.Models.Errors;

public class RocketOutOfPlatformException : ArgumentException
{
    public RocketOutOfPlatformException() : base()
    {
    }

    public RocketOutOfPlatformException(string? message) : base(message)
    {
    }

    public RocketOutOfPlatformException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public RocketOutOfPlatformException(string? message, string? paramName) : base(message, paramName)
    {
    }

    public RocketOutOfPlatformException(string? message, string? paramName, Exception? innerException) : base(message, paramName, innerException)
    {
    }
}
