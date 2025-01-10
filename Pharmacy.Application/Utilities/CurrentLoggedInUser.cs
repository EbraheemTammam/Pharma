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
        UserId = httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Sid) ?? string.Empty;
        _userManager = userManager;
    }
    public string UserId { get; }
    public async Task<User> GetUser() =>
        (await _userManager.FindByIdAsync(UserId))!;
}
