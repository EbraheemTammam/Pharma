namespace Pharmacy.Domain.Models.ProductsModule;


public sealed class IncomingOrder: BaseModel<Guid>
{
    public decimal Price {get; set;}
    public decimal Paid {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public Guid ProviderId {get; set;}
    public ProductProvider? Provider {get; set;}
    public IEnumerable<ProductItem>? Products {get; set;}
}
