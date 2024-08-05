using System.ComponentModel.DataAnnotations;
using Pharmacy.Shared.ProductsModule.Validations;

namespace Pharmacy.Shared.DTOs.ProductsModule;



public abstract record ScarceProductBaseDTO
{
    [Required, MaxLength(250)]
    public required string name {get; set;}

    [Required, PositiveNumber]
    public int amount;
}

public record ScarceProductCreateDTO: ScarceProductBaseDTO
{

}

public record ScarceProductDTO: ScarceProductBaseDTO
{
    public Guid id {get; set;}
}
