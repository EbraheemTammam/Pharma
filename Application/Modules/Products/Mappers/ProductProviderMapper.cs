using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Shared.Modules.Products.DTOs;

namespace Pharmacy.Application.Modules.Products.Mappers;



public static class ProductProviderMapper
{
    public static ProductProvider ToModel(this ProductProviderCreateDTO schema) =>
        new()
        {
            Name = schema.Name,
            Indepted = 0
        };


    public static ProductProviderDTO ToDTO(this ProductProvider model) =>
        new()
        {
            Id = model.Id,
            Name = model.Name,
            Indepted = (decimal)model.Indepted
        };

    public static void Update(this ProductProvider productProvider, ProductProviderCreateDTO schema)
    {
        productProvider.Name = schema.Name;
    }
}
