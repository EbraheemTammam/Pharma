using Microsoft.AspNetCore.Identity;
using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Models;


public sealed class User : IdentityUser<int>, BaseModel
{
    public required string FirstName {get; set;}
    public required string LastName {get; set;}
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string GetFullName() => $"{FirstName} {LastName}";
}
