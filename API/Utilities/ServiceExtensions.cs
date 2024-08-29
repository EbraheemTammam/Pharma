using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Service.Interfaces;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Data.Repositories;
using Pharmacy.Application.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Pharmacy.Presentation.Utilities;



public static class ServiceExtensions
{
    public static void Configure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.ConfigureDbContextPool(configuration);
        services.ConfigureAuth(configuration);
        services.ConfigureCors();
        services.ConfigureIISIntegration();
        services.ConfigureRepositories();
        services.ConfigureServices();
        services.AddControllers();
    }

    private static void ConfigureDbContextPool(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<ApplicationDbContext>(
            options => options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")
            )
        );
    }

    private static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        /* ------- Register Identity ------- */
        services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        /* ------- Configure Cookies ------- */
        services.ConfigureApplicationCookie(
                    options =>
                    {
                        options.LoginPath = "/api/auth/Login";
                        options.LogoutPath = "/api/auth/Logout";
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                        options.SlidingExpiration = true;
                        options.AccessDeniedPath = "/api/auth/AccessDenied";
                    }
                );
        /* ------- Add Authentication ------- */
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
        /* ------- Add Authorization ------- */
        services.AddAuthorization();
        /* ------- Get Default User Data from appsettings.json ------- */
        services.Configure<User>(configuration.GetSection("DefaultUserModel"));
    }

    private static void ConfigureCors(this IServiceCollection services) =>
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

    private static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options => {}
    );

    private static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IRepository<IncomingOrder>, IncomingOrderRepository>();
        services.AddScoped<IProductItemRepository, ProductItemRepository>();
        services.AddScoped<IRepository<Order>, OrderRepository>();
        services.AddScoped<IRepository<OrderItem>, OrderItemRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepositry>();
    }

    private static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<UserManager<User>, UserManager<User>>();
        services.AddScoped<PasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IScarceProductService, ScarceProductService>();
        services.AddScoped<IProductProviderService, ProductProviderService>();
        services.AddScoped<IIncomingOrderService, IncomingOrderService>();
        services.AddScoped<ICustomerService, CustomerService>();
    }
}
