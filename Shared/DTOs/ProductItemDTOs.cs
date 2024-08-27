using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Pharmacy.Shared.Validations;

namespace Pharmacy.Shared.DTOs;



public abstract record ProductItemBaseDTO
{
    [Required]
    public DateOnly ExpirationDate {get; set;}

    [Required, NonNegative]
    public int NumberOfBoxes {get; set;}
}

public record ProductItemCreateDTO : ProductItemBaseDTO
{
    [OneRequired(nameof(ProductId)), MaxLength(15)]
    public string? Barcode {get; set;}

    [OneRequired(nameof(Barcode))]
    public Guid? ProductId {get; set;}
}

public record ProductItemDTO : ProductItemBaseDTO
{
    public int Id {get; set;}
    public required string ProductName {get; set;}
    public decimal Price {get; set;}
}
