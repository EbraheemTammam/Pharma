using Pharmacy.Services;
using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Shared.Modules.Products.DTOs;
using Pharmacy.Application.Utilities;
using Pharmacy.Application.Modules.Products.Mappers;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Shared.Generics;
namespace Pharmacy.Application.Modules.Products.Services;



public class ProductService : IProductService
{
    private readonly IRepositoryManager _repositoryManager;

    public ProductService(IRepositoryManager repoManager) =>
        _repositoryManager = repoManager;

    public BaseResponse GetAll() =>
        new OkResponse<IEnumerable<ProductDTO>>(
            _repositoryManager.Products.GetAll().ConvertAll(obj => obj.ToDTO())
        );

    public BaseResponse GetById(Guid id)
    {
        Product? product = _repositoryManager.Products.GetById(id);
        return product is null ? new NotFoundResponse(id, nameof(Product))
        : new OkResponse<ProductDTO>(product.ToDTO());
    }

    public BaseResponse GetByBarcode(string barcode)
    {
        Product? product = _repositoryManager.Products.GetByBarcode(barcode);
        return product is null ? new NotFoundResponse(barcode, nameof(Product), "barcode")
        : new OkResponse<ProductDTO>(product.ToDTO());
    }

    public BaseResponse Create(ProductCreateDTO schema)
    {
        Product product = schema.ToModel();
        product.OwnedElements = 0;
        _repositoryManager.Products.Add(product);
        _repositoryManager.Save();
        return new OkResponse<ProductDTO>(product.ToDTO());
    }

    public BaseResponse Update(Guid id, ProductCreateDTO schema)
    {
        Product? product = _repositoryManager.Products.GetById(id);
        if(product is null) return new NotFoundResponse(id, nameof(Product));
        product.Update(schema);
        _repositoryManager.Products.Update(product);
        _repositoryManager.Save();
        return new OkResponse<ProductDTO>(product.ToDTO());
    }

    public BaseResponse Delete(Guid id)
    {
        Product? product = _repositoryManager.Products.GetById(id);
        if(product is null) return new NotFoundResponse(id, nameof(Product));
        _repositoryManager.Products.Delete(product);
        _repositoryManager.Save();
        return new NoContentResponse();
    }
}
