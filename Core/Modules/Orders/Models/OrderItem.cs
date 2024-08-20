using Pharmacy.Domain.Generics;
using Pharmacy.Domain.Modules.Products.Models;

namespace Pharmacy.Domain.Modules.Orders.Models;


public sealed class OrderItem : BaseModel<int>
{
    public int Amount {get; set;}
    public decimal Price {get; set;}
    public Guid ProductId {get; set;}
    public Guid OrderId {get; set;}
    public required Product Product {get; set;}
    public int Remainig() => this.Product.OwnedElements;
}
