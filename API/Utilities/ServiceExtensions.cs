using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Services.IncomingOrdersModule;
using Pharmacy.Application.Services.ProductsModule;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Infrastructure.Data.Repositories;
using Pharmacy.Infrastructure.Generics;
using Pharmacy.Infrastructure.Generics.Repositories;
using Pharmacy.Services;

namespace Pharmacy.Presentation.Utilities;

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
        services.AddScoped(typeof(IRepository<IncomingOrder>), typeof(IncomingOrderRepository));
        services.AddScoped(typeof(IRepository<ProductItem>), typeof(ProductItemRepository));
        services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
        services.RegisterDbContextPool(configuration);
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductProviderService, ProductProviderService>();
        services.AddScoped<IIncomingOrderService, IncomingOrderService>();
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
