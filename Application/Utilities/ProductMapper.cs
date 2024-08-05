using Pharmacy.Domain.Models.ProductsModule;
using Pharmacy.Shared.DTOs.ProductsModule;

namespace Pharmacy.Application.Utilities;



public static class ProductMapper
{
    public static Product ToModel(this ProductCreateDTO schema) =>
        new()
        {
            Name = schema.name,
            NumberOfElements = schema.number_of_elements,
            Barcode = schema.barcode,
            PricePerElement = schema.price_per_element,
            IsLack = schema.lack,
            Minimum = schema.minimum
        };


    public static ProductDTO ToDTO(this Product model) =>
        new()
        {
            id = model.Id,
            barcode = model.Barcode,
            name = model.Name,
            number_of_elements = model.NumberOfElements,
            price_per_element = model.PricePerElement,
            lack = model.IsLack,
            minimum = model.Minimum,
            owned_elements = model.OwnedElements,
            boxes_owned = (int) Math.Ceiling(
                (decimal) model.OwnedElements / model.NumberOfElements
            )
        };
}
