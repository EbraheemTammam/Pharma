using Microsoft.Extensions.DependencyInjection;
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
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IScarceProductService, ScarceProductService>();
        services.AddScoped<IProductProviderService, ProductProviderService>();
        services.AddScoped<IIncomingOrderService, IncomingOrderService>();
        services.AddScoped<ICustomerService, CustomerService>();
        return services;
    }
}
