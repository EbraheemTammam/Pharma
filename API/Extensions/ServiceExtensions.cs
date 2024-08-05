using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Services.ProductsModule;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.Repositories;
using Pharmacy.Services;

namespace Pharmacy.Presentation.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );
        }
    );

    public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options => {}
    );

    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.RegisterDbContextPool(configuration);
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddScoped<IProductService, ProductService>();
    }

    private static void RegisterDbContextPool(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<ApplicationDbContext>(
            options => {
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")
                );
            }
        );
    }
}
