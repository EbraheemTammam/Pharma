using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Shared.Modules.Products.DTOs;
using Pharmacy.Application.Utilities;
using Pharmacy.Application.Modules.Products.Mappers;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Shared.Generics;
using Pharmacy.Services.Modules.Products;
namespace Pharmacy.Application.Modules.Products.Services;



public class ProductProviderService : IProductProviderService
{
    private readonly IRepositoryManager _manager;

    public ProductProviderService(IRepositoryManager repoManager)
    {
        _manager = repoManager;
    }

    public async Task<BaseResponse> GetAll() =>
        new OkResponse<IEnumerable<ProductProviderDTO>>(
            (await _manager.ProductProviders.GetAll()).ConvertAll(obj => obj.ToDTO())
        );

    public async Task<BaseResponse> GetById(Guid id)
    {
        ProductProvider? productProvider = await _manager.ProductProviders.GetById(id);
        return productProvider is null ? new NotFoundResponse(id, nameof(ProductProvider))
        : new OkResponse<ProductProviderDTO>(productProvider.ToDTO());
    }

    public async Task<BaseResponse> Create(ProductProviderCreateDTO schema)
    {
        ProductProvider productProvider = await _manager.ProductProviders.Add(schema.ToModel());
        await _manager.Save();
        return new OkResponse<ProductProviderDTO>(productProvider.ToDTO());
    }

    public async Task<BaseResponse> Update(Guid id, ProductProviderCreateDTO schema)
    {
        ProductProvider? productProvider = await _manager.ProductProviders.GetById(id);
        if(productProvider is null) return new NotFoundResponse(id, nameof(ProductProvider));
        productProvider.Update(schema);
        productProvider = _manager.ProductProviders.Update(productProvider);
        await _manager.Save();
        return new OkResponse<ProductProviderDTO>(productProvider.ToDTO());
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
