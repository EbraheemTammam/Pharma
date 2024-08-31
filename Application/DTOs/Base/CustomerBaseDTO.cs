using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Pharmacy.Application.DTOs;



public abstract record CustomerBaseDTO
{
    [Required, MinLength(3), MaxLength(100)]
    public required string Name {get; set;}

    [AllowNull, DefaultValue(0)]
    public decimal Dept {get; set;}
}
