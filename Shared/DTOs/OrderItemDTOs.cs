using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Pharmacy.Shared.Validations;

namespace Pharmacy.Shared.DTOs;


public abstract record OrderItemBaseDTO
{
    [Required, NonNegative]
    public int Amount {get; set;}
}

public record OrderItemCreateDTO : OrderItemBaseDTO
{
    [AllowNull]
    public Guid? ProductId {get; set;}

    [AllowNull]
    public string? ProductBarcode {get; set;}
}

public record OrderItemDTO : OrderItemBaseDTO
{
    public Guid Id {get; set;}
    public decimal Price {get; set;}
    public required string ProductName {get; set;}
    public int RemainedItems {get; set;}
}
