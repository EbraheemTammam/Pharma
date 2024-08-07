using System.ComponentModel.DataAnnotations;
using Pharmacy.Shared.Modules.Products.Validations;

namespace Pharmacy.Shared.Modules.Products.DTOs;



public abstract record ScarceProductBaseDTO
{
    [Required, MaxLength(250)]
    public required string Name {get; set;}

    [Required, PositiveNumber]
    public int Amount;
}

public record ScarceProductCreateDTO: ScarceProductBaseDTO
{

}

public record ScarceProductDTO: ScarceProductBaseDTO
{
    public Guid Id {get; set;}
}
