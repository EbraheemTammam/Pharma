using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Models;


public sealed class ProductProvider : BaseModel<Guid>
{
    public required string Name {get; set;}
    public double? Indepted {get; set;}
}
