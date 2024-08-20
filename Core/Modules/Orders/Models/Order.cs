using Pharmacy.Domain.Generics;
using Pharmacy.Domain.Modules.Users.Models;

namespace Pharmacy.Domain.Modules.Orders.Models;


public sealed class Order : BaseModel<Guid>
{
    public DateTime CreatedAt {get; set;}
    public decimal Paid {get; set;}
    public decimal TotalPrice {get; set;}
    public int UserId {get; set;}
    public Guid CustomerId {get; set;}
    public required CustomUser CreatedBy {get; set;}
    public required Customer Customer {get; set;}
    public IEnumerable<OrderItem>? Items {get; set;}
}
