using Microsoft.AspNetCore.Identity;

namespace Pharmacy.Domain.Modules.Users.Models;


public class CustomUser : IdentityUser<int>
{
    public required string FirstName {get; set;}
    public required string LastName {get; set;}
    public bool IsAdmin {get; set;}
}
