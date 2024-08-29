using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Pharmacy.Domain.Models;

namespace Pharmacy.Presentation.Utilities;


public static class WebAppExtensions
{
    public static void Configure(this WebApplication app)
    {
        app.Lifetime.ApplicationStarted.Register(async () => await app.PreLoadDefaultUser());
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
    private static async Task PreLoadDefaultUser(this WebApplication app)
    {
        using(var scope = app.Services.CreateAsyncScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            User user = scope.ServiceProvider.GetRequiredService<IOptions<User>>().Value;
            await CreateUserIfNotExist(userManager, user);
        }
    }

    private static async Task CreateUserIfNotExist(UserManager<User> userManager, User user)
    {
        if(!userManager.Users.Any())
        {
            var result = await userManager.CreateAsync(user);
            if(!result.Succeeded)
            {
                foreach(var e in result.Errors)
                    Console.WriteLine(e.Description);
            }
        }
    }
}
