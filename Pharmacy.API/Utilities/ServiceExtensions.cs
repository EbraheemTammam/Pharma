using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Models;
using Pharmacy.Application.Utilities;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Utilities;

namespace Pharmacy.Presentation.Utilities;

public static class ServiceExtensions
{
    public static IServiceCollection Configure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.ConfigureDbContextPool(configuration);
        services.AddIdentityConfiguration(configuration);
        services.AddAuthentication();
        services.AddAuthorization();
        services.AddJWTAuthentication(configuration);
        services.ConfigureCors();
        services.ConfigureIISIntegration();
        services.AddControllers();
        services.AddRepositories();
        services.AddServices();
        return services;
    }

    public static IServiceCollection ConfigureDbContextPool(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContextPool<ApplicationDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );

    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        /* ------- Register Identity ------- */
        services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        /* ------- Get Default User Data from appsettings.json ------- */
        var defaultUserModel = configuration.GetSection("DefaultUserModel");
        if (defaultUserModel.Exists())
            services.Configure<User>(defaultUserModel);
        return services;
    }

    public static IServiceCollection AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettingSection = configuration.GetSection("JwtSetting");
        if (!jwtSettingSection.Exists()) return services;
        services.Configure<JwtSetting>(jwtSettingSection);
        var jwtSetting = jwtSettingSection.Get<JwtSetting>();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSetting!.ValidIssuer,
                // ValidAudience = jwtSetting.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecretKey))
            };
        });
        return services;
    }

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
