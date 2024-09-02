using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Models;


public sealed class Order : BaseModel<Guid>
{
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public double Paid {get; set;}
    public double TotalPrice {get; set;}
    public int UserId {get; set;}
    public Guid? CustomerId {get; set;}
    public User? CreatedBy {get; set;}
    public Customer? Customer {get; set;}
    public IEnumerable<OrderItem>? Items {get; set;}
}
