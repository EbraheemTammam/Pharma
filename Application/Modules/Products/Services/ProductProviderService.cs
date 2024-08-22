using Pharmacy.Services;
using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Shared.Modules.Products.DTOs;
using Pharmacy.Application.Utilities;
using Pharmacy.Application.Modules.Products.Mappers;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Shared.Generics;
namespace Pharmacy.Application.Modules.Products.Services;



public class ProductProviderService : IProductProviderService
{
    private readonly IRepositoryManager _repositoryManager;

    public ProductProviderService(IRepositoryManager repoManager)
    {
        _repositoryManager = repoManager;
    }

    public BaseResponse GetAll() =>
        new OkResponse<IEnumerable<ProductProviderDTO>>(
            _repositoryManager.ProductProviders.GetAll().ConvertAll(obj => obj.ToDTO())
        );

    public BaseResponse GetById(Guid id)
    {
        ProductProvider? productProvider = _repositoryManager.ProductProviders.GetById(id);
        return productProvider is null ? new NotFoundResponse(id, nameof(ProductProvider))
        : new OkResponse<ProductProviderDTO>(productProvider.ToDTO());
    }

    public BaseResponse Create(ProductProviderCreateDTO schema)
    {
        ProductProvider productProvider = _repositoryManager.ProductProviders.Add(schema.ToModel());
        _repositoryManager.Save();
        return new OkResponse<ProductProviderDTO>(productProvider.ToDTO());
    }

    public BaseResponse Update(Guid id, ProductProviderCreateDTO schema)
    {
        ProductProvider? productProvider = _repositoryManager.ProductProviders.GetById(id);
        if(productProvider is null) return new NotFoundResponse(id, nameof(ProductProvider));
        productProvider.Update(schema);
        productProvider = _repositoryManager.ProductProviders.Update(productProvider);
        _repositoryManager.Save();
        return new OkResponse<ProductProviderDTO>(productProvider.ToDTO());
    }

    public BaseResponse Delete(Guid id)
    {
        ProductProvider? productProvider = _repositoryManager.ProductProviders.GetById(id);
        if(productProvider is null) return new NotFoundResponse(id, nameof(ProductProvider));
        _repositoryManager.ProductProviders.Delete(productProvider);
        _repositoryManager.Save();
        return new NoContentResponse();
    }
}
