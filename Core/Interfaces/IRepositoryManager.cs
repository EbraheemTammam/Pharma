using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Domain.Modules.Orders.Models;

namespace Pharmacy.Domain.Interfaces;


public interface IRepositoryManager : IDisposable
{
    IProductRepository Products {get; }
    IRepository<ProductProvider> ProductProviders {get; }
    IRepository<IncomingOrder> IncomingOrders {get; }
    IRepository<ProductItem> ProductItems {get; }
    IRepository<Customer> Customers {get; }
    IRepository<Order> Orders {get; }
    IRepository<ScarceProduct> ScarceProducts {get; }
    void Save();
}
