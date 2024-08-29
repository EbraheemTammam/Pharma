using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Pharmacy.Shared.DTOs;


public abstract record UserBaseDTO
{
    [Required, MaxLength(150)]
    public required string FirstName {get; set;}

    [Required, MaxLength(150)]
    public required string LastName {get; set;}

    [Required, MaxLength(150), EmailAddress]
    public required string Email {get; set;}
}

public record UserCreateDTO : UserBaseDTO
{
    [AllowNull, DefaultValue(false)]
    public bool IsManager {get; set;}

    [Required]
    public required string Password {get; set;}
}

public record UserDTO : UserBaseDTO
{
    public int Id {get; set;}
}

public record LoginDTO
{
    [Required, MaxLength(100), EmailAddress]
    public required string Email {get; set;}

    [Required]
    public required string Password {get; set;}
}
