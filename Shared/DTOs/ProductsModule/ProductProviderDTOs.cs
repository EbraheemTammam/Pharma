using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Shared.DTOs.ProductsModule;



public abstract record ProductProviderBaseDTO
{
    [Required, MaxLength(100)]
    public required string name {get; set;}
}

public record ProductProviderCreateDTO: ProductProviderBaseDTO
{

}

public record ProductProviderDTO: ProductProviderBaseDTO
{
    public Guid id {get; set;}
    public decimal indepted {get; set;}
}
