using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Modules.Orders.Services;
using Pharmacy.Application.Modules.Products.Services;
using Pharmacy.Application.Modules.ScarceProducts.Services;
using Pharmacy.Application.Modules.Users.Services;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Domain.Modules.Users.Models;
using Pharmacy.Infrastructure.Data.Repositories;
using Pharmacy.Infrastructure.Generics;
using Pharmacy.Infrastructure.Generics.Repositories;
using Pharmacy.Infrastructure.Modules.Products.Data.Repositories;
using Pharmacy.Services.Modules.Orders;
using Pharmacy.Services.Modules.Products;
using Pharmacy.Services.Modules.Users;

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
        services.RegisterDbContextPool(configuration);
        services.AddIdentity<CustomUser, IdentityRole<int>>().AddEntityFrameworkStores<ApplicationDbContext>();
        //  Inject Repos
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IRepository<IncomingOrder>, IncomingOrderRepository>();
        services.AddScoped<IRepository<ProductItem>, ProductItemRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        // Inject Services
        services.AddScoped<UserManager<CustomUser>, UserManager<CustomUser>>();
        services.AddScoped<PasswordHasher<CustomUser>, PasswordHasher<CustomUser>>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IScarceProductService, ScarceProductService>();
        services.AddScoped<IProductProviderService, ProductProviderService>();
        services.AddScoped<IIncomingOrderService, IncomingOrderService>();
        services.AddScoped<ICustomerService, CustomerService>();
        //  Get Default User Data from appsettings.json
        services.Configure<CustomUser>(configuration.GetSection("defaultUserModel"));
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
