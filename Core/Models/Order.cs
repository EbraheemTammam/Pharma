using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Models;


public sealed class Order : BaseModel<Guid>
{
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public decimal Paid {get; set;}
    public decimal TotalPrice {get; set;}
    public int UserId {get; set;}
    public Guid CustomerId {get; set;}
    public required CustomUser CreatedBy {get; set;}
    public required Customer Customer {get; set;}
    public IEnumerable<OrderItem>? Items {get; set;}
}
