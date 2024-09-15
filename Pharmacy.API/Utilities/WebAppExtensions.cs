using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Pharmacy.Domain.Models;

namespace Pharmacy.Presentation.Utilities;


public static class WebAppExtensions
{
    public static void Configure(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Lifetime.ApplicationStarted.Register(async () => await app.PreLoadDefaultData());
    }
    private static async Task PreLoadDefaultData(this WebApplication app)
    {
        using(var scope = app.Services.CreateAsyncScope())
        {
            /* ------- Load Default Roles ------- */
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            await roleManager.CreateRolesIfNotExist(["Employee", "Manager", "Admin"]);
            /* ------- Load Default User ------- */
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            User user = scope.ServiceProvider.GetRequiredService<IOptions<User>>().Value;
            await userManager.CreateUserIfNotExist(user, "Admin");
        }
    }

    private static async Task CreateUserIfNotExist(this UserManager<User> userManager, User user, string role)
    {
        User? existingUser = await userManager.FindByIdAsync(user.Id.ToString());
        if(existingUser is not null) return;
        var result = await userManager.CreateAsync(user);
        if(!result.Succeeded)
        {
            foreach(var e in result.Errors) Console.WriteLine(e.Description);
        }
        await userManager.AddToRoleAsync(user, role);
    }

    private static async Task CreateRolesIfNotExist(this RoleManager<IdentityRole<int>> roleManager, string[] roles)
    {
        foreach(string role in roles)
        {
            if(await roleManager.RoleExistsAsync(role)) continue;
            var result = await roleManager.CreateAsync(new IdentityRole<int>(role));
            if(result.Succeeded) continue;
            foreach(var e in result.Errors) Console.WriteLine(e.Description);
        }
    }
}