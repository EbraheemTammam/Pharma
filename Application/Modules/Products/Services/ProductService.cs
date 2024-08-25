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
    private readonly IRepositoryManager _manager;

    public ProductService(IRepositoryManager repoManager) =>
        _manager = repoManager;

    public async Task<BaseResponse> GetAll() =>
        new OkResponse<IEnumerable<ProductDTO>>
        (
            (await _manager.Products.GetAll()).ConvertAll(obj => obj.ToDTO())
        );

    public async Task<BaseResponse> GetById(Guid id)
    {
        Product? product = await _manager.Products.GetById(id);
        return product is null ? new NotFoundResponse(id, nameof(Product))
        : new OkResponse<ProductDTO>(product.ToDTO());
    }

    public async Task<BaseResponse> GetByBarcode(string barcode)
    {
        Product? product = await _manager.Products.GetByBarcode(barcode);
        return product is null ? new NotFoundResponse(barcode, nameof(Product), "barcode")
        : new OkResponse<ProductDTO>(product.ToDTO());
    }

    public async Task<BaseResponse> Create(ProductCreateDTO schema)
    {
        Product product = schema.ToModel();
        product.OwnedElements = 0;
        await _manager.Products.Add(product);
        await _manager.Save();
        return new OkResponse<ProductDTO>(product.ToDTO());
    }

    public async Task<BaseResponse> Update(Guid id, ProductCreateDTO schema)
    {
        Product? product = await _manager.Products.GetById(id);
        if(product is null) return new NotFoundResponse(id, nameof(Product));
        product.Update(schema);
        _manager.Products.Update(product);
        await _manager.Save();
        return new OkResponse<ProductDTO>(product.ToDTO());
    }

    public async Task<BaseResponse> Delete(Guid id)
    {
        Product? product = await _manager.Products.GetById(id);
        if(product is null) return new NotFoundResponse(id, nameof(Product));
        _manager.Products.Delete(product);
        await _manager.Save();
        return new NoContentResponse();
    }
}
