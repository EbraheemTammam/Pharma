using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Domain.Modules.Orders.Models;

namespace Pharmacy.Domain.Interfaces;


public interface IRepositoryManager : IDisposable
{
    IProductRepository Products {get; }
    IRepository<ProductProvider> ProductProviders {get; }
    IRepository<ProductItem> ProductItems {get; }
    IRepository<IncomingOrder> IncomingOrders {get; }
    IRepository<ScarceProduct> ScarceProducts {get; }
    IRepository<Customer> Customers {get; }
    IPaymentRepository Payments {get; }
    IRepository<Order> Orders {get; }
    IRepository<OrderItem> OrderItems {get; }
    Task Save();
}
