using Pharmacy.Services.Modules.Products;
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

    public async Task<BaseResponse> GetAll() =>
        new OkResponse<IEnumerable<ProductDTO>>(
            (await _repositoryManager.Products.GetAll()).ConvertAll(obj => obj.ToDTO())
        );

    public async Task<BaseResponse> GetById(Guid id)
    {
        Product? product = await _repositoryManager.Products.GetById(id);
        return product is null ? new NotFoundResponse(id, nameof(Product))
        : new OkResponse<ProductDTO>(product.ToDTO());
    }

    public async Task<BaseResponse> GetByBarcode(string barcode)
    {
        Product? product = await _repositoryManager.Products.GetByBarcode(barcode);
        return product is null ? new NotFoundResponse(barcode, nameof(Product), "barcode")
        : new OkResponse<ProductDTO>(product.ToDTO());
    }

    public async Task<BaseResponse> Create(ProductCreateDTO schema)
    {
        Product product = schema.ToModel();
        product.OwnedElements = 0;
        await _repositoryManager.Products.Add(product);
        await _repositoryManager.Save();
        return new OkResponse<ProductDTO>(product.ToDTO());
    }

    public async Task<BaseResponse> Update(Guid id, ProductCreateDTO schema)
    {
        Product? product = await _repositoryManager.Products.GetById(id);
        if(product is null) return new NotFoundResponse(id, nameof(Product));
        product.Update(schema);
        _repositoryManager.Products.Update(product);
        await _repositoryManager.Save();
        return new OkResponse<ProductDTO>(product.ToDTO());
    }

    public async Task<BaseResponse> Delete(Guid id)
    {
        Product? product = await _repositoryManager.Products.GetById(id);
        if(product is null) return new NotFoundResponse(id, nameof(Product));
        _repositoryManager.Products.Delete(product);
        await _repositoryManager.Save();
        return new NoContentResponse();
    }
}
