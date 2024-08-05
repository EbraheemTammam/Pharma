using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Pharmacy.Shared.ProductsModule.Validations;

namespace Pharmacy.Shared.DTOs.ProductsModule;



public abstract record ProductBaseDTO
{
    [AllowNull, MaxLength(15)]
    public string? barcode {get; set;}

    [Required, MaxLength(100)]
    public required string name {get; set;}

    [Required, PositiveNumber]
    public int number_of_elements {get; set;}

    [Required, PositiveNumber]
    public double price_per_element {get; set;}

    [Required, DefaultValue(false)]
    public bool lack {get; set;} = false;

    [Required, PositiveNumber, DefaultValue(0)]
    public int minimum {get; set;} = 0;
}

public record ProductCreateDTO: ProductBaseDTO
{

}

public record ProductDTO: ProductBaseDTO
{
    public Guid id {get; set;}
    public int owned_elements {get; set;}
    public int boxes_owned {get; set;}
}
