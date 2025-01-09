using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Application.DTOs;

public record BaseAuthDTO
{
    [Required]
    public required string Username { get; init; }

    [Required]
    public required string Password { get; init; }
}
