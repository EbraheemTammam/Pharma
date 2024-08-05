using Pharmacy.Domain.Models.ProductsModule;
using Pharmacy.Domain.Models.OrdersModule;
using Pharmacy.Domain.Models.UsersModule;

namespace Pharmacy.Domain.Interfaces;


public interface IRepositoryManager: IDisposable
{
    IRepository<Product> Products {get; }
    IRepository<ProductProvider> ProductProviders {get; }
    IRepository<IncomingOrder> IncomingOrders {get; }
    IRepository<ProductItem> ProductItems {get; }
    // IRepository<Customer> Customers {get; }
    // IRepository<Order> Orders {get; }
    // IRepository<ScarceProduct> ScarceProducts {get; }
    // IRepository<User> Users {get; }
}
