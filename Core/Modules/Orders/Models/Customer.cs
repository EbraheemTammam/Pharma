using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Modules.Orders.Models;


public sealed class Customer : BaseModel<Guid>
{
    public required string Name {get; set;}
    public double Dept {get; set;} = 0;
    public IEnumerable<Payment>? Payments {get; set;}
}
