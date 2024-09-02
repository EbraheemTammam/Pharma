namespace Pharmacy.Application.DTOs;



public record ProductProviderDTO : ProductProviderBaseDTO
{
    public Guid Id {get; set;}
    public decimal Indepted {get; set;}
}
