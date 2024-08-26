using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Modules.Orders.Models;


public sealed class Payment : BaseModel<int>
{
    public double Paid {get; set;}
    public DateTime CreatedAt {get; set;}
    public required Guid CustomerId {get; set;}
}
