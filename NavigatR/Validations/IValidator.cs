using System.ComponentModel.DataAnnotations;

namespace NavigatR.Validations;

public interface IValidator<in T>
{
    public ValidationResult Validate(T? _, ValidationContext validationContext);
}