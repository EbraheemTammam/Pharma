using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Application.DTOs;



public abstract record ProductProviderBaseDTO
{
    [Required, MinLength(3), MaxLength(100)]
    public required string Name {get; set;}
}
