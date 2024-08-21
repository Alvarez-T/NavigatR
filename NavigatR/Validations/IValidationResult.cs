using System.ComponentModel.DataAnnotations;

namespace NavigatR.Validations;

public interface IValidationResult<out T>
{
    public ValidationResult ValidationResult { get; }
    public T? BasedOn { get; }

}