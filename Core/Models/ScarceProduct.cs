using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Models;


public sealed class ScarceProduct : BaseModel<Guid>
{
    public required string Name {get; set;}
    public int Amount {get; set;}
}
