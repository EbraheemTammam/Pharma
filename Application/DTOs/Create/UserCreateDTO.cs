using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Pharmacy.Application.DTOs;



public record UserCreateDTO : UserBaseDTO
{
    [AllowNull, DefaultValue(false)]
    public bool IsManager {get; set;}

    [Required]
    public required string Password {get; set;}
}
