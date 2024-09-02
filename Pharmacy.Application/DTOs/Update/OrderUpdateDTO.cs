using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Application.DTOs;



public record OrderUpdateDTO : OrderBaseDTO
{
    [Required]
    public required IEnumerable<OrderItemCreateDTO> Items {get; set;}
}
