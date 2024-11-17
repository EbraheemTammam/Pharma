using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Models;
using Pharmacy.Application.Utilities;
using Pharmacy.Infrastructure.Utilities;
using Pharmacy.Infrastructure.Data;

namespace Pharmacy.Presentation.Utilities;



public static class ServiceExtensions
{
    public static IServiceCollection Configure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.ConfigureDbContextPool(configuration);
        services.ConfigureAuth(configuration);
        services.ConfigureCookie();
        services.ConfigureCors();
        services.ConfigureIISIntegration();
        services.AddControllers();
        services.AddServices();
        services.AddRepositories();
        return services;
    }

    public static IServiceCollection ConfigureDbContextPool(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContextPool<ApplicationDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );

    public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        /* ------- Register Identity ------- */
        services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        /* ------- Add Authentication And Authorization ------- */
        services.AddAuthorization()
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
        /* ------- Get Default User Data from appsettings.json ------- */
        services.Configure<User>(configuration.GetSection("DefaultUserModel"));
        return services;
    }

    public static IServiceCollection ConfigureCookie(this IServiceCollection services) =>
        services.ConfigureApplicationCookie(
                    options =>
                    {
                        options.LoginPath = "/Api/Auth/Login";
                        options.LogoutPath = "/Api/Auth/Logout";
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                        options.SlidingExpiration = true;
                        options.AccessDeniedPath = "/Api/Auth/AccessDenied";
                        options.Cookie.HttpOnly = true;
                        options.Cookie.SameSite = SameSiteMode.None;
                        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    }
                );

    public static IServiceCollection ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
            {
                options.AddPolicy(
                    "CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                );
            }
        );

    public static IServiceCollection ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options => {});
}
