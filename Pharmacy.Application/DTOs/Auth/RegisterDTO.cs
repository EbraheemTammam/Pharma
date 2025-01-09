using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Application.DTOs;

public record RegisterDTO : BaseAuthDTO
{
    [Required(ErrorMessage = "First name is required.")]
    [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
    [MinLength(3, ErrorMessage = "First name must be at least 3 characters.")]
    public required string FirstName { get; init; }

    [Required(ErrorMessage = "Last name is required.")]
    [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
    [MinLength(3, ErrorMessage = "Last name must be at least 3 characters.")]
    public required string LastName { get; init; }

    [Required]
    public bool IsManager { get; init; }
}
