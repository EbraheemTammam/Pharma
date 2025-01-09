namespace Pharmacy.Application.DTOs;



public record ProductItemDTO : ProductItemBaseDTO
{
    public int Id {get; set;}
    public required string ProductName {get; set;}
    public decimal Price {get; set;}
    public string? ProductBarcode {get; set;}
}
