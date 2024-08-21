using NavigatR.Validations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace NavigatR.Validations;

public class Validation<TValidator, T> : ValidationAttribute where TValidator : IValidator<T>
{
    public Validation()
    {

    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var teste = (TValidation)validationContext.ObjectInstance;
        ExecuteValidation(teste, otherInjectedHere);
    }

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "Validate")]
    private static extern ValidationResult ExecuteValidation(IValidator<T> validator, T validation);
}