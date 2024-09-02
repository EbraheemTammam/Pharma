namespace Pharmacy.Application.DTOs;



public record ProductDTO : ProductBaseDTO
{
    public Guid Id {get; set;}
    public int OwnedElements {get; set;}
    public int BoxesOwned {get; set;}
}
