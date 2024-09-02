namespace Pharmacy.Application.DTOs;



public record OrderDTO : OrderBaseDTO
{
    public Guid Id {get; set;}
    public DateTime CreatedAt {get; set;}
    public decimal TotalPrice {get; set;}
    public string? CustomerName {get; set;}
    public required string CreatedBy {get; set;}
}
