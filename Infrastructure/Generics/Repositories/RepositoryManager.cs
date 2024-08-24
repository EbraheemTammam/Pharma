using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Domain.Modules.Orders.Models;
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
    public IProductRepository Products => _serviceProvider.GetRequiredService<IProductRepository>();
    public IRepository<ProductProvider> ProductProviders => _serviceProvider.GetRequiredService<IRepository<ProductProvider>>();
    public IRepository<IncomingOrder> IncomingOrders => _serviceProvider.GetRequiredService<IRepository<IncomingOrder>>();
    public IRepository<ProductItem> ProductItems => _serviceProvider.GetRequiredService<IRepository<ProductItem>>();
    public IRepository<ScarceProduct> ScarceProducts => _serviceProvider.GetRequiredService<IRepository<ScarceProduct>>();
    public IRepository<Customer> Customers => _serviceProvider.GetRequiredService<IRepository<Customer>>();
    public IRepository<Order> Orders => _serviceProvider.GetRequiredService<IRepository<Order>>();
    public void Dispose() => _context.Dispose();
    public async Task Save() => await _context.SaveChangesAsync();
}
