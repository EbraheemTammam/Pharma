using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Application.DTOs;



public record ProductItemCreateDTO : ProductItemBaseDTO
{
    [Required]
    public Guid ProductId {get; set;}
}
