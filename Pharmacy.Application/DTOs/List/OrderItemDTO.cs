namespace Pharmacy.Application.DTOs;

public record OrderItemDTO : OrderItemBaseDTO
{
    public int Id {get; set;}
    public decimal Price {get; set;}
    public required string ProductName {get; set;}
    public Guid ProductId {get; set;}
    public string? ProductBarcode {get; set;}
    public int RemainedItems {get; set;}
    public decimal TotalPrice {get; set;}
    public bool ProductIsLack {get; set;}
}
