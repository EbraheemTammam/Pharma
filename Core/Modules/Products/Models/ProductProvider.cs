using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Modules.Products.Models;


public sealed class ProductProvider : BaseModel<Guid>
{
    public required string Name {get; set;}
    public IEnumerable<IncomingOrder>? IncomingOrders {get; set;}
    public decimal Indepted() =>
        this.IncomingOrders is null ?
        0 : this.IncomingOrders.Sum(order => order.Price - order.Paid);
}
