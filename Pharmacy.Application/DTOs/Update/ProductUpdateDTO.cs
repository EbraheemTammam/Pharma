using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Pharmacy.Application.Validations;

namespace Pharmacy.Application.DTOs;

public record ProductUpdateDTO
{
    [MaxLength(15)]
    public string? Barcode {get; set;}

    [MinLength(3), MaxLength(100)]
    public string? Name {get; set;}

    [NonNegative, DefaultValue(1)]
    public int? NumberOfElements {get; set;}

    [NonNegative]
    public double? PricePerElement {get; set;}

    [NonNegative, DefaultValue(0)]
    public int? Minimum {get; set;}

    [DefaultValue(false)]
    public bool? IsLack {get; set;}
}
