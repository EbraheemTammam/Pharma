using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Modules.Orders.Models;


public sealed class Payment : BaseModel<int>
{
    public decimal Paid {get; set;} = 0;
    public DateTime CreatedAt {get;} = DateTime.UtcNow;
    public required Guid CustomerId {get; set;}
}
