using Pharmacy.Domain.Models;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Mappers;



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

    public static void Update(this ScarceProduct product, ScarceProductCreateDTO schema)
    {
        product.Name = schema.Name;
        product.Amount = schema.Amount;
    }
}
