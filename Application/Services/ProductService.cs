using Pharmacy.Application.Mappers;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Utilities;

namespace Pharmacy.Application.Services;



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

    public async Task<BaseResponse> GetLacked() =>
        new OkResponse<IEnumerable<ProductDTO>>
        (
            (await _manager.Products.Filter(obj => obj.IsLack)).ConvertAll(obj => obj.ToDTO())
        );

    public async Task<BaseResponse> GetById(Guid id)
    {
        Product? product = await _manager.Products.GetById(id);
        return product is null ? new NotFoundResponse(id, nameof(Product))
        : new OkResponse<ProductDTO>(product.ToDTO());
    }

    public async Task<BaseResponse> Create(ProductCreateDTO schema)
    {
        Product product = schema.ToModel();
        product.OwnedElements = 0;
        await _manager.Products.Add(product);
        await _manager.Save();
        return new CreatedResponse<ProductDTO>(product.ToDTO());
    }

    public async Task<BaseResponse> Update(Guid id, ProductCreateDTO schema)
    {
        Product? product = await _manager.Products.GetById(id);
        if(product is null) return new NotFoundResponse(id, nameof(Product));
        product.Update(schema);
        _manager.Products.Update(product);
        await _manager.Save();
        return new CreatedResponse<ProductDTO>(product.ToDTO());
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
