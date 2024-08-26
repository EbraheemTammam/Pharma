using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Pharmacy.Domain.Models;

namespace Pharmacy.Application.Utilities;


public static class WebAppExtensions
{
    public async static Task PreLoadDefaultUser(this WebApplication app)
    {
        using(var scope = app.Services.CreateAsyncScope())
        {
            var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            User _user = scope.ServiceProvider.GetRequiredService<IOptions<User>>().Value;
            if(!_userManager.Users.Any())
            {
                var result = await _userManager.CreateAsync(_user);
                if(!result.Succeeded)
                {
                    foreach (var e in result.Errors)
                        Console.WriteLine(e.Description);
                }
            }
        }
    }
}
