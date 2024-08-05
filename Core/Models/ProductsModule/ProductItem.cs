namespace Pharmacy.Domain.Models.ProductsModule;


public sealed class ProductItem: BaseModel<int>
{
    public DateOnly ExpirationDate {get; set;}
    public int NumberOfElements {get; set;}
    public int NumberOfBoxes {get; set;}
    public Guid ProductId {get; set;}
    public Guid IncomingOrderId {get; set;}
    public Product? Product {get; set;}
    public IncomingOrder? IncomingOrder {get; set;}
    public bool Sold() => this.NumberOfElements == 0;
    public double Price() =>
        Product is null ? 0
        : NumberOfBoxes * Product.NumberOfElements * Product.PricePerElement;
}
