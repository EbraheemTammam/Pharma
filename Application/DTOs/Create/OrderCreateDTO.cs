using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Application.DTOs;



public record OrderCreateDTO : OrderUpdateDTO
{
    [Required]
    public required Guid CustomerId {get; set;}
}
