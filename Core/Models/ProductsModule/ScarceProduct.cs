namespace Pharmacy.Domain.Models.ProductsModule;


public sealed class ScarceProduct: BaseModel<Guid>
{
    public required string Name {get; set;}
    public int Amount {get; set;}
}
