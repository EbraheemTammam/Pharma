using Microsoft.Extensions.DependencyInjection;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Models;
using Pharmacy.Infrastructure.Repositories;

namespace Pharmacy.Infrastructure.Utilities;



public static class DependencyInjection
{
    /// <summary>
    ///     Inject all the repositories
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <returns>A reference to this instance after injecting repositories</returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IRepository<IncomingOrder>, IncomingOrderRepository>();
        services.AddScoped<IProductItemRepository, ProductItemRepository>();
        services.AddScoped<IRepository<Order>, OrderRepository>();
        services.AddScoped<IRepository<OrderItem>, OrderItemRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepositry>();
        return services;
    }
}
