using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Modules.Products.Models;


public sealed class ScarceProduct : BaseModel<Guid>
{
    public required string Name {get; set;}
    public int Amount {get; set;}
}
