using Pharmacy.Services.Modules.Products;
using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Shared.Modules.Products.DTOs;
using Pharmacy.Application.Utilities;
using Pharmacy.Application.Modules.Products.Mappers;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Shared.Generics;
namespace Pharmacy.Application.Modules.ScarceProducts.Services;



public class ScarceProductService : IScarceProductService
{
    private readonly IRepositoryManager _manager;

    public ScarceProductService(IRepositoryManager repoManager) =>
        _manager = repoManager;

    public async Task<BaseResponse> GetAll() =>
        new OkResponse<IEnumerable<ScarceProductDTO>>(
            (await _manager.ScarceProducts.GetAll()).ConvertAll(obj => obj.ToDTO())
        );

    public async Task<BaseResponse> GetById(Guid id)
    {
        ScarceProduct? product = await _manager.ScarceProducts.GetById(id);
        return product is null ? new NotFoundResponse(id, nameof(ScarceProduct))
        : new OkResponse<ScarceProductDTO>(product.ToDTO());
    }

    public async Task<BaseResponse> Create(ScarceProductCreateDTO schema)
    {
        ScarceProduct product = schema.ToModel();
        await _manager.ScarceProducts.Add(product);
        await _manager.Save();
        return new OkResponse<ScarceProductDTO>(product.ToDTO());
    }

    public async Task<BaseResponse> Update(Guid id, ScarceProductCreateDTO schema)
    {
        ScarceProduct? product = await _manager.ScarceProducts.GetById(id);
        if(product is null) return new NotFoundResponse(id, nameof(ScarceProduct));
        product.Update(schema);
        _manager.ScarceProducts.Update(product);
        await _manager.Save();
        return new OkResponse<ScarceProductDTO>(product.ToDTO());
    }

    public async Task<BaseResponse> Delete(Guid id)
    {
        ScarceProduct? product = await _manager.ScarceProducts.GetById(id);
        if(product is null) return new NotFoundResponse(id, nameof(Product));
        _manager.ScarceProducts.Delete(product);
        await _manager.Save();
        return new NoContentResponse();
    }
}
