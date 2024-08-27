using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Service.Interfaces;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.Repositories;
using Pharmacy.Application.Services;

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
        services.AddIdentity<User, IdentityRole<int>>().AddEntityFrameworkStores<ApplicationDbContext>();
        //  Inject Repos
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IRepository<IncomingOrder>, IncomingOrderRepository>();
        services.AddScoped<IProductItemRepository, ProductItemRepository>();
        services.AddScoped<IRepository<Order>, OrderRepository>();
        services.AddScoped<IRepository<OrderItem>, OrderItemRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepositry>();
        // Inject Services
        services.AddScoped<UserManager<User>, UserManager<User>>();
        services.AddScoped<PasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IScarceProductService, ScarceProductService>();
        services.AddScoped<IProductProviderService, ProductProviderService>();
        services.AddScoped<IIncomingOrderService, IncomingOrderService>();
        services.AddScoped<ICustomerService, CustomerService>();
        //  Get Default User Data from appsettings.json
        services.Configure<User>(configuration.GetSection("DefaultUserModel"));
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
