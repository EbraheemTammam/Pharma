using Pharmacy.Domain.Models;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Mappers;



public static class ProductMapper
{
    public static Product ToModel(this ProductCreateDTO schema) =>
        new()
        {
            Name = schema.Name,
            NumberOfElements = schema.NumberOfElements ?? 1,
            Barcode = schema.Barcode,
            PricePerElement = schema.PricePerElement,
            IsLack = false,
            Minimum = schema.Minimum
        };

    public static ProductDTO ToDTO(this Product model) =>
        new()
        {
            Id = model.Id,
            Barcode = model.Barcode,
            Name = model.Name,
            NumberOfElements = model.NumberOfElements,
            PricePerElement = model.PricePerElement,
            IsLack = model.IsLack,
            Minimum = model.Minimum,
            OwnedElements = model.OwnedElements,
            BoxesOwned = (int) Math.Ceiling(
                (decimal) model.OwnedElements / model.NumberOfElements
            )
        };

    public static void Update(this Product product, ProductCreateDTO schema)
    {
        product.Barcode = schema.Barcode;
        product.Name = schema.Name;
        product.NumberOfElements = schema.NumberOfElements ?? product.NumberOfElements;
        product.PricePerElement = schema.PricePerElement;
        product.Minimum = schema.Minimum;
    }
}
