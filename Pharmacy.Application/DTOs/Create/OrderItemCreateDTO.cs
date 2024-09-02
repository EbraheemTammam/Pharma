using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Application.DTOs;



public record OrderItemCreateDTO : OrderItemBaseDTO
{
    [Required]
    public Guid ProductId {get; set;}
}
