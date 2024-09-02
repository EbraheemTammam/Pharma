using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Application.DTOs;



public record IncomingOrderCreateDTO : IncomingOrderBaseDTO
{
    [Required]
    public Guid ProviderId {get; set;}

    [Required]
    public required IEnumerable<ProductItemCreateDTO> ProductItems {get; set;}
}
