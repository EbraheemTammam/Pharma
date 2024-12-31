using Microsoft.Extensions.DependencyInjection;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Models;
using Pharmacy.Infrastructure.Data;

namespace Pharmacy.Infrastructure.Repositories;


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
    public IRepository<Payment> Payments => _serviceProvider.GetRequiredService<IRepository<Payment>>();
    public IRepository<Order> Orders => _serviceProvider.GetRequiredService<IRepository<Order>>();
    public IRepository<OrderItem> OrderItems => _serviceProvider.GetRequiredService<IRepository<OrderItem>>();
    public void Dispose() => _context.Dispose();
    public async Task Save() => await _context.SaveChangesAsync();
}
