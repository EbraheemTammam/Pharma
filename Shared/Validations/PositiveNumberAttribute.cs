using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Numerics;


namespace Pharmacy.Shared.ProductsModule.Validations;



class PositiveNumberAttribute: ValidationAttribute
{
    public PositiveNumberAttribute(): base("Value must be greater than 0"){}
    public override bool IsValid(object? obj)
    {
        if(obj is int IntVal && IntVal < 0) return false;
        if(obj is decimal DecimalVal && DecimalVal < 0) return false;
        if(obj is double DoubleVal && DoubleVal < 0) return false;
        return true;
    }
}
