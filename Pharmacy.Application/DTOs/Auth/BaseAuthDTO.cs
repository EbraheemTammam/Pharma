using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Application.DTOs;

public record BaseAuthDTO
{
    [Required, EmailAddress]
    public required string Email { get; init; }

    [Required]
    public required string Password { get; init; }
}
