using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Pharmacy.Shared.Modules.Products.Validations;

namespace Pharmacy.Shared.Modules.Products.DTOs;



public abstract record IncomingOrderBaseDTO
{
    [Required, PositiveNumber]
    public decimal Price {get; set;}

    [Required, PositiveNumber, DefaultValue(0)]
    public decimal Paid {get; set;} = 0;
}

public record IncomingOrderCreateDTO: IncomingOrderBaseDTO
{
    [Required]
    public Guid ProviderId {get; set;}
}

public record IncomingOrderDTO: IncomingOrderBaseDTO
{
    public Guid Id {get; set;}
    public DateTime CreatedAt {get; set;}
    public required ProductProviderDTO Provider {get; set;}
}
