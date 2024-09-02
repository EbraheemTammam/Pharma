using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Pharmacy.Application.Validations;

namespace Pharmacy.Application.DTOs;



public abstract record ProductBaseDTO
{
    [AllowNull, MaxLength(15)]
    public string? Barcode {get; set;}

    [Required, MinLength(3), MaxLength(100)]
    public required string Name {get; set;}

    [NonNegative, DefaultValue(1)]
    public int? NumberOfElements {get; set;}

    [Required, NonNegative]
    public double PricePerElement {get; set;}

    [AllowNull, DefaultValue(false)]
    public bool? IsLack {get; set;}

    [Required, NonNegative, DefaultValue(0)]
    public int Minimum {get; set;}
}
