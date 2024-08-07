using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Domain.Modules.Orders.Models;
using Pharmacy.Domain.Modules.Users.Models;
using Pharmacy.Infrastructure.Generics;
using Microsoft.Extensions.DependencyInjection;

namespace Pharmacy.Infrastructure.Data.Repositories;


public class RepositoryManager : IRepositoryManager
{
    private readonly ApplicationDbContext _context;
    private IServiceProvider _serviceProvider;
    public RepositoryManager(ApplicationDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }
    public IRepository<Product> Products => _serviceProvider.GetRequiredService<IRepository<Product>>();
    public IRepository<ProductProvider> ProductProviders => _serviceProvider.GetRequiredService<IRepository<ProductProvider>>();
    public IRepository<IncomingOrder> IncomingOrders => _serviceProvider.GetRequiredService<IRepository<IncomingOrder>>();
    public IRepository<ProductItem> ProductItems => _serviceProvider.GetRequiredService<IRepository<ProductItem>>();
    public IRepository<ScarceProduct> ScarceProducts => _serviceProvider.GetRequiredService<IRepository<ScarceProduct>>();
    public IRepository<Customer> Customers => _serviceProvider.GetRequiredService<IRepository<Customer>>();
    public IRepository<Order> Orders => _serviceProvider.GetRequiredService<IRepository<Order>>();
    public IRepository<User> Users => _serviceProvider.GetRequiredService<IRepository<User>>();
    public void Dispose() => _context.Dispose();
}
