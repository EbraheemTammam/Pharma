using Microsoft.AspNetCore.Identity;

namespace Pharmacy.Domain.Modules.Users.Models;


public sealed class CustomUser : IdentityUser<int>
{
    public required string FirstName {get; set;}
    public required string LastName {get; set;}
}
