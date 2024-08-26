using System.ComponentModel.DataAnnotations;


namespace Pharmacy.Shared.Validations;



class NonNegativeAttribute : ValidationAttribute
{
    public NonNegativeAttribute(): base("Value must be greater than 0"){}
    public override bool IsValid(object? obj)
    {
        if(obj is int IntVal && IntVal < 0) return false;
        if(obj is decimal DecimalVal && DecimalVal < 0) return false;
        if(obj is double DoubleVal && DoubleVal < 0) return false;
        return true;
    }
}
