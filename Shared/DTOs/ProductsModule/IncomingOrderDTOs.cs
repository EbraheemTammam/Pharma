using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Pharmacy.Shared.ProductsModule.Validations;

namespace Pharmacy.Shared.DTOs.ProductsModule;



public abstract record IncomingOrderBaseDTO
{
    [Required, PositiveNumber]
    public decimal price {get; set;}

    [Required, PositiveNumber, DefaultValue(0)]
    public decimal paid {get; set;} = 0;
}

public record IncomingOrderCreateDTO: IncomingOrderBaseDTO
{
    [Required]
    public Guid company {get; set;}
}

public record IncomingOrderDTO: IncomingOrderBaseDTO
{
    public Guid id {get; set;}
    public DateTime time {get; set;}
    public required ProductProviderDTO company {get; set;}
}
