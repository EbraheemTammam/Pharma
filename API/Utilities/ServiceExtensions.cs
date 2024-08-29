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
    public static void Configure(this IServiceCollection services, IConfiguration configuration) =>
        services.AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .ConfigureDbContextPool(configuration)
                .ConfigureAuth(configuration)
                .ConfigureCors()
                .ConfigureIISIntegration()
                .ConfigureRepositories()
                .ConfigureServices()
                .AddControllers();

    private static IServiceCollection ConfigureDbContextPool(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContextPool<ApplicationDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );

    private static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
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
                )
        /* ------- Register Identity ------- */
                .AddIdentity<User, IdentityRole<int>>()
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

    private static IServiceCollection ConfigureCors(this IServiceCollection services) =>
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

    private static IServiceCollection ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options => {});

    /// <summary>
    ///     Inject all the repositories
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <returns>A reference to this instance after injecting services</returns>
    private static IServiceCollection ConfigureRepositories(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>()
                .AddScoped(typeof(IRepository<>), typeof(GenericRepository<>))
                .AddScoped<IRepository<IncomingOrder>, IncomingOrderRepository>()
                .AddScoped<IProductItemRepository, ProductItemRepository>()
                .AddScoped<IRepository<Order>, OrderRepository>()
                .AddScoped<IRepository<OrderItem>, OrderItemRepository>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IPaymentRepository, PaymentRepositry>();

    /// <summary>
    ///     Inject all the services
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <returns>A reference to this instance after injecting services</returns>
    private static IServiceCollection ConfigureServices(this IServiceCollection services) =>
        services.AddScoped<UserManager<User>, UserManager<User>>()
                .AddScoped<PasswordHasher<User>, PasswordHasher<User>>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IScarceProductService, ScarceProductService>()
                .AddScoped<IProductProviderService, ProductProviderService>()
                .AddScoped<IIncomingOrderService, IncomingOrderService>()
                .AddScoped<ICustomerService, CustomerService>();
}
