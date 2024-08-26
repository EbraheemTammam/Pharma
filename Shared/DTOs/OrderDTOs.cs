using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Pharmacy.Shared.DTOs;


public abstract record OrderBaseDTO
{
    [AllowNull]
    public decimal Paid {get; set;}
}

public record OrderCreateDTO : OrderBaseDTO
{
    [Required]
    public required IEnumerable<OrderItemCreateDTO> Items {get; set;}
}

public record OrderDTO : OrderBaseDTO
{
    public DateTime CreatedAt {get; set;}
    public decimal TotalPrice {get; set;}
    public required string CustomerName {get; set;}
    public required string CreatedBy {get; set;}
    public required IEnumerable<OrderItemDTO> Items {get; set;}
}
