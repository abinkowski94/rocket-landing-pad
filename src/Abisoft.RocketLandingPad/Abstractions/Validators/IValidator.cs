namespace Abisoft.RocketLandingPad.Abstractions.Validators;

internal interface IValidator<in T>
{
    Exception? Validate(T? value);
}
