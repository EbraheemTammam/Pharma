using Pharmacy.Application.Mappers;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Utilities;

namespace Pharmacy.Application.Services;



public class ProductProviderService : IProductProviderService
{
    private readonly IRepositoryManager _manager;

    public ProductProviderService(IRepositoryManager repoManager)
    {
        _manager = repoManager;
    }

    public async Task<BaseResponse> GetAll() =>
        new OkResponse<IEnumerable<ProductProviderDTO>>
        (
            (await _manager.ProductProviders.GetAll())
            .ConvertAll(obj => obj.ToDTO())
        );

    public async Task<BaseResponse> GetById(Guid id)
    {
        ProductProvider? productProvider = await _manager.ProductProviders.GetById(id);
        return productProvider switch
        {
            null => new NotFoundResponse(id, nameof(ProductProvider)),
            _ => new OkResponse<ProductProviderDTO>(productProvider.ToDTO())
        };
    }

    public async Task<BaseResponse> Create(ProductProviderCreateDTO schema)
    {
        ProductProvider productProvider = await _manager.ProductProviders.Add(schema.ToModel());
        await _manager.Save();
        return new CreatedResponse<ProductProviderDTO>(productProvider.ToDTO());
    }

    public async Task<BaseResponse> Update(Guid id, ProductProviderCreateDTO schema)
    {
        ProductProvider? productProvider = await _manager.ProductProviders.GetById(id);
        if(productProvider is null) return new NotFoundResponse(id, nameof(ProductProvider));
        productProvider.Update(schema);
        productProvider = _manager.ProductProviders.Update(productProvider);
        await _manager.Save();
        return new CreatedResponse<ProductProviderDTO>(productProvider.ToDTO());
    }

    public async Task<BaseResponse> Delete(Guid id)
    {
        ProductProvider? productProvider = await _manager.ProductProviders.GetById(id);
        if(productProvider is null) return new NotFoundResponse(id, nameof(ProductProvider));
        _manager.ProductProviders.Delete(productProvider);
        await _manager.Save();
        return new NoContentResponse();
    }
}
