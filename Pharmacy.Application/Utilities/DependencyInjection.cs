using Microsoft.Extensions.DependencyInjection;
using Pharmacy.API.Utilities;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.Services;

namespace Pharmacy.Application.Utilities;



public static class DependencyInjection
{
    /// <summary>
    ///     Inject all the services
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <returns>A reference to this instance after injecting services</returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IScarceProductService, ScarceProductService>();
        services.AddScoped<IProductProviderService, ProductProviderService>();
        services.AddScoped<IIncomingOrderService, IncomingOrderService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICurrentLoggedInUser, CurrentLoggedInUser>();
        return services;
    }
}
