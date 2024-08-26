using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Models;


public sealed class OrderItem : BaseModel<int>
{
    public int Amount {get; set;}
    public double Price {get; set;}
    public Guid ProductId {get; set;}
    public Guid OrderId {get; set;}
    public required Product Product {get; set;}
}
