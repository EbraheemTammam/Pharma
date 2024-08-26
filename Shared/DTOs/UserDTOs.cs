using System.ComponentModel.DataAnnotations;

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
    [Required]
    public required string Password {get; set;}
}

public record UserDTO : UserBaseDTO
{
    public int Id {get; set;}
}
