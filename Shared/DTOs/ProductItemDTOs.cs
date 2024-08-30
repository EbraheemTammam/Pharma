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
    [Required]
    public Guid ProductId {get; set;}
}

public record ProductItemDTO : ProductItemBaseDTO
{
    public int Id {get; set;}
    public required string ProductName {get; set;}
    public decimal Price {get; set;}
}
