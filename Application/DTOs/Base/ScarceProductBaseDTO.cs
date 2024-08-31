using System.ComponentModel.DataAnnotations;
using Pharmacy.Application.Validations;

namespace Pharmacy.Application.DTOs;



public abstract record ScarceProductBaseDTO
{
    [Required, MinLength(3), MaxLength(250)]
    public required string Name {get; set;}

    [Required, NonNegative]
    public int Amount {get; set;}
}
