using System.ComponentModel.DataAnnotations;
using Pharmacy.Shared.Validations;

namespace Pharmacy.Shared.DTOs;



public abstract record ScarceProductBaseDTO
{
    [Required, MaxLength(250)]
    public required string Name {get; set;}

    [Required, NonNegative]
    public int Amount {get; set;}
}

public record ScarceProductCreateDTO : ScarceProductBaseDTO
{

}

public record ScarceProductDTO : ScarceProductBaseDTO
{
    public Guid Id {get; set;}
}
