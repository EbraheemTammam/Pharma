using System.ComponentModel.DataAnnotations;


namespace Pharmacy.Shared.Validations;



public class OneRequiredAttribute : ValidationAttribute
{
    private readonly string _otherPropertyName;

    public OneRequiredAttribute(string  otherPropertyName) =>
        _otherPropertyName = otherPropertyName;

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherPropertyName);
        if (otherPropertyInfo is null)
            return new ValidationResult($"Unknown property: {_otherPropertyName}");

        var otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);

        if (value is null && otherValue is null)
            return new ValidationResult($"Either '{validationContext.DisplayName}' or '{_otherPropertyName}' must be provided.");

        return ValidationResult.Success!;
    }
}
