using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Pharmacy.Shared.Modules.Products.Validations;

namespace Pharmacy.Shared.Modules.Products.DTOs;



public abstract record ProductBaseDTO
{
    [AllowNull, MaxLength(15)]
    public string? Barcode {get; set;}

    [Required, MaxLength(100)]
    public required string Name {get; set;}

    [Required, PositiveNumber]
    public int NumberOfElements {get; set;}

    [Required, PositiveNumber]
    public double PricePerElement {get; set;}

    [Required, DefaultValue(false)]
    public bool IsLack {get; set;} = false;

    [Required, PositiveNumber, DefaultValue(0)]
    public int Minimum {get; set;} = 0;
}

public record ProductCreateDTO: ProductBaseDTO
{

}

public record ProductDTO: ProductBaseDTO
{
    public Guid Id {get; set;}
    public int OwnedElements {get; set;}
    public int BoxesOwned {get; set;}
}
