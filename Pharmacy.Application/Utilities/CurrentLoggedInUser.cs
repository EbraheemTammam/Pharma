using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Application.Interfaces;
using Pharmacy.Domain.Models;

namespace Pharmacy.API.Utilities;

public class CurrentLoggedInUser : ICurrentLoggedInUser
{
    private readonly UserManager<User> _userManager;
    public CurrentLoggedInUser(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
    {
        UserName = httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
        _userManager = userManager;
    }
    public string UserName { get; }
    public async Task<User> GetUser() =>
        (await _userManager.FindByNameAsync(UserName))!;
}
