using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Models;


public sealed class Payment : BaseModel<int>
{
    public double Paid {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public required Guid CustomerId {get; set;}
}
