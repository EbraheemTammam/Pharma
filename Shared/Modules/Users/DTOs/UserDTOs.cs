using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Shared.Modules.Users.DTOs;


public class UserBaseDTO
{
    [Required, MaxLength(150)]
    public required string FirstName {get; set;}

    [Required, MaxLength(150)]
    public required string LastName {get; set;}

    [Required, MaxLength(150), EmailAddress]
    public required string Email {get; set;}
}

public class UserCreateDTO : UserBaseDTO
{
    [Required]
    public required string Password {get; set;}
}

public class UserDTO : UserBaseDTO
{
    public int Id {get; set;}
}
