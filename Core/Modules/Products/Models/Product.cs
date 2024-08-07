using Pharmacy.Domain.Generics;

namespace Pharmacy.Domain.Modules.Products.Models;

public sealed class Product : BaseModel<Guid>
{
    public required string Name {get; set;}
    public int NumberOfElements {get; set;}
    public double PricePerElement {get; set;}
    public string? Barcode {get; set;}
    public int OwnedElements {get; set;}
    public bool IsLack {get; set;}
    public int Minimum {get; set;}
}
