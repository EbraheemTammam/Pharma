using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Application.DTOs;



public record LoginDTO
{
    [Required, MaxLength(100), EmailAddress]
    public required string Email {get; set;}

    [Required]
    public required string Password {get; set;}
}
