using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Pharmacy.Shared.DTOs;


public abstract record OrderBaseDTO
{
    [AllowNull]
    public decimal? Paid {get; set;}
}

public record OrderUpdateDTO : OrderBaseDTO
{
    [Required]
    public required IEnumerable<OrderItemCreateDTO> Items {get; set;}
}

public record OrderCreateDTO : OrderUpdateDTO
{
    [Required]
    public required Guid CustomerId {get; set;}
}

public record OrderDTO : OrderBaseDTO
{
    public Guid Id {get; set;}
    public DateTime CreatedAt {get; set;}
    public decimal TotalPrice {get; set;}
    public required string CustomerName {get; set;}
    public required string CreatedBy {get; set;}
}
