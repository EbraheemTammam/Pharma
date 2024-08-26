using Pharmacy.Domain.Models;
using Pharmacy.Shared.DTOs;

namespace Pharmacy.Application.Mappers;



public static class ProductItemMapper
{
    public static ProductItem ToModel(this ProductItemCreateDTO schema, Product product, Guid incomingOrderId) =>
        new()
        {
           ExpirationDate = schema.ExpirationDate,
           NumberOfBoxes = schema.NumberOfBoxes,
           ProductId = product.Id,
           NumberOfElements = product.NumberOfElements * schema.NumberOfBoxes,
           IncomingOrderId = incomingOrderId
        };

    public static ProductItemDTO ToDTO(this ProductItem model) =>
        new()
        {
            Id = model.Id,
            NumberOfBoxes = model.NumberOfBoxes,
            ProductName = model.Product!.Name,
            Price = (decimal)(model.NumberOfBoxes * model.NumberOfElements * model.Product.PricePerElement)
        };
}
