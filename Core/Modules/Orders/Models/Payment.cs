using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Modules.Orders.Models;


public sealed class Payment : BaseModel<int>
{
    public double Paid {get; set;} = 0;
    public DateTime CreatedAt {get;} = DateTime.UtcNow;
    public required Guid CustomerId {get; set;}
}
