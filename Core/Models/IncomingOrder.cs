using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Models;


public sealed class IncomingOrder : BaseModel<Guid>
{
    public double Price {get; set;}
    public double Paid {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public Guid ProviderId {get; set;}
    public ProductProvider? Provider {get; set;}
    public IEnumerable<ProductItem>? Items {get; set;}
}
