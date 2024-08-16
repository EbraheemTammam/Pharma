using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Modules.Products.Models;


public sealed class ProductItem : BaseModel<int>
{
    public DateOnly ExpirationDate {get; set;}
    public int NumberOfElements {get; set;}
    public int NumberOfBoxes {get; set;}
    public Guid ProductId {get; set;}
    public Guid IncomingOrderId {get; set;}
    public Product? Product {get; set;}
    public IncomingOrder? IncomingOrder {get; set;}
}
