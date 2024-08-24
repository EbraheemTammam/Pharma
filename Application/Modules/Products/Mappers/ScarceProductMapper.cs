using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Shared.Modules.Products.DTOs;

namespace Pharmacy.Application.Modules.Products.Mappers;



public static class ScarceProductMapper
{
    public static ScarceProduct ToModel(this ScarceProductCreateDTO schema) =>
        new()
        {
            Name = schema.Name,
            Amount = schema.Amount
        };

    public static ScarceProductDTO ToDTO(this ScarceProduct model) =>
        new()
        {
            Id = model.Id,
            Name = model.Name,
            Amount = model.Amount
        };

    public static ScarceProduct Update(this ScarceProduct product, ScarceProductCreateDTO schema)
    {
        product.Name = schema.Name;
        product.Amount = schema.Amount;
        return product;
    }
}
