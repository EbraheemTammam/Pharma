using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Pharmacy.Shared.ProductsModule.Validations;

namespace Pharmacy.Shared.DTOs.ProductsModule;



public abstract record ProductItemBaseDTO
{
    [AllowNull, MaxLength(15)]
    public string? barcode {get; set;}

    [Required]
    public DateOnly expiration {get; set;}

    [Required, PositiveNumber]
    public int number_of_elements {get; set;}
}

public record ProductItemCreateDTO: ProductItemBaseDTO
{
    [AllowNull]
    public Guid type {get; set;}
}

public record ProductItemDTO: ProductItemBaseDTO
{
    public int id {get; set;}
    public required ProductDTO type {get; set;}
    public int number_of_boxes {get; set;}
    public bool sold;
    public required string company;
    public decimal price;
}
