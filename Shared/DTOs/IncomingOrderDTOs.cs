using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Pharmacy.Shared.Validations;

namespace Pharmacy.Shared.DTOs;



public abstract record IncomingOrderBaseDTO
{
    [Required, NonNegative]
    public decimal Price {get; set;}

    [Required, NonNegative, DefaultValue(0)]
    public decimal Paid {get; set;} = 0;
}

public record IncomingOrderCreateDTO : IncomingOrderBaseDTO
{
    [Required]
    public Guid ProviderId {get; set;}

    [Required]
    public required IEnumerable<ProductItemCreateDTO> ProductItems {get; set;}
}

public record IncomingOrderUpdateDTO : IncomingOrderBaseDTO
{

}

public record IncomingOrderDTO : IncomingOrderBaseDTO
{
    public Guid Id {get; set;}
    public DateTime CreatedAt {get; set;}
    public required string ProviderName {get; set;}
}
