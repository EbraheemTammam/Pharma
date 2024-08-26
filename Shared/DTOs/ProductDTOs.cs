using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Pharmacy.Shared.Validations;

namespace Pharmacy.Shared.DTOs;



public abstract record ProductBaseDTO
{
    [AllowNull, MaxLength(15)]
    public string? Barcode {get; set;}

    [Required, MaxLength(100)]
    public required string Name {get; set;}

    [NonNegative, DefaultValue(1)]
    public int? NumberOfElements {get; set;} = 1;

    [Required, NonNegative]
    public double PricePerElement {get; set;}

    [DefaultValue(false)]
    public bool? IsLack {get; set;} = false;

    [Required, NonNegative, DefaultValue(0)]
    public int Minimum {get; set;} = 0;
}

public record ProductCreateDTO : ProductBaseDTO
{

}

public record ProductDTO : ProductBaseDTO
{
    public Guid Id {get; set;}
    public int OwnedElements {get; set;}
    public int BoxesOwned {get; set;}
}
