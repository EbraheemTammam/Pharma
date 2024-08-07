using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Shared.Modules.Products.DTOs;



public abstract record ProductProviderBaseDTO
{
    [Required, MaxLength(100)]
    public required string Name {get; set;}
}

public record ProductProviderCreateDTO: ProductProviderBaseDTO
{

}

public record ProductProviderDTO: ProductProviderBaseDTO
{
    public Guid Id {get; set;}
    public decimal Indepted {get; set;}
}
