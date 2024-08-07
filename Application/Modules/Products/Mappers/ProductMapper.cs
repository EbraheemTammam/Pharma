using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Shared.Modules.Products.DTOs;

namespace Pharmacy.Application.Modules.Products.Mappers;



public static class ProductMapper
{
    public static Product ToModel(this ProductCreateDTO schema) =>
        new()
        {
            Name = schema.Name,
            NumberOfElements = schema.NumberOfElements,
            Barcode = schema.Barcode,
            PricePerElement = schema.PricePerElement,
            IsLack = schema.IsLack,
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

    public static Product Update(this Product product, ProductCreateDTO schema)
    {
        product.Barcode = schema.Barcode;
        product.Name = schema.Name;
        product.NumberOfElements = schema.NumberOfElements;
        product.PricePerElement = schema.PricePerElement;
        product.IsLack = schema.IsLack;
        product.Minimum = schema.Minimum;
        return product;
    }
}
