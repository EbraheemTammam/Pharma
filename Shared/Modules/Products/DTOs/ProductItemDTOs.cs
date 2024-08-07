using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Pharmacy.Shared.Modules.Products.Validations;

namespace Pharmacy.Shared.Modules.Products.DTOs;



public abstract record ProductItemBaseDTO
{
    [AllowNull, MaxLength(15)]
    public string? Barcode {get; set;}

    [Required]
    public DateOnly ExpirationDate {get; set;}

    [Required, PositiveNumber]
    public int NumberOfElements {get; set;}
}

public record ProductItemCreateDTO: ProductItemBaseDTO
{
    [AllowNull]
    public Guid ProductId {get; set;}
}

public record ProductItemDTO: ProductItemBaseDTO
{
    public int Id {get; set;}
    public required ProductDTO Product {get; set;}
    public int NumberOfBoxes {get; set;}
    public bool Sold;
    public required string ProviderName;
    public decimal Price;
}
