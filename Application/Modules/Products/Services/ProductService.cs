using Pharmacy.Services;
using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Shared.Modules.Products.DTOs;
using Pharmacy.Application.Utilities;
using Pharmacy.Application.Modules.Products.Mappers;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Shared.Generics;
namespace Pharmacy.Application.Services.ProductsModule;



public class ProductService : IProductService
{
    private readonly IRepositoryManager _repositoryManager;

    public ProductService(IRepositoryManager repoManager)
    {
        _repositoryManager = repoManager;
    }

    public BaseResponse GetAll() =>
        new OkResponse<IEnumerable<ProductDTO>>(
            _repositoryManager.Products.GetAll().ConvertAll(obj => obj.ToDTO())
        );

    public BaseResponse GetById(int id)
    {
        Product? product = _repositoryManager.Products.GetById(id);
        return product is null ? new NotFoundResponse(id, nameof(Product))
        : new OkResponse<ProductDTO>(product.ToDTO());
    }

    public BaseResponse Create(ProductCreateDTO schema)
    {
        Product product = _repositoryManager.Products.Add(schema.ToModel());
        _repositoryManager.Products.Save();
        return new OkResponse<ProductDTO>(product.ToDTO());
    }

    public BaseResponse Update(int id, ProductCreateDTO schema)
    {
        Product? product = _repositoryManager.Products.GetById(id);
        if(product is null) return new NotFoundResponse(id, nameof(Product));
        product.Update(schema);
        product = _repositoryManager.Products.Update(product);
        _repositoryManager.Products.Save();
        return new OkResponse<ProductDTO>(product.ToDTO());
    }

    public BaseResponse Remove(int id)
    {
        Product? product = _repositoryManager.Products.GetById(id);
        if(product is null) return new NotFoundResponse(id, nameof(Product));
        _repositoryManager.Products.Remove(product);
        _repositoryManager.Products.Save();
        return new OkResponse<bool>(true);
    }
}