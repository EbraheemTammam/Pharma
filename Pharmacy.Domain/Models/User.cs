using Microsoft.AspNetCore.Identity;

namespace Pharmacy.Domain.Models;


public sealed class User : IdentityUser<int>
{
    public required string FirstName {get; set;}
    public required string LastName {get; set;}
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string GetFullName() => $"{FirstName} {LastName}";
}
