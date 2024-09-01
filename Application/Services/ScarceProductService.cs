using Pharmacy.Application.Mappers;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Utilities;

namespace Pharmacy.Application.Services;



public class ScarceProductService : IScarceProductService
{
    private readonly IRepositoryManager _manager;

    public ScarceProductService(IRepositoryManager repoManager) =>
        _manager = repoManager;

    public async Task<BaseResponse> GetAll() =>
        new OkResponse<IEnumerable<ScarceProductDTO>>
        (
            (await _manager.ScarceProducts.GetAll())
            .ConvertAll(obj => obj.ToDTO())
        );

    public async Task<BaseResponse> GetById(Guid id)
    {
        ScarceProduct? product = await _manager.ScarceProducts.GetById(id);
        return product switch
        {
            null => new NotFoundResponse(id, nameof(ScarceProduct)),
            _ => new OkResponse<ScarceProductDTO>(product.ToDTO())
        };
    }

    public async Task<BaseResponse> Create(ScarceProductCreateDTO schema)
    {
        ScarceProduct product = schema.ToModel();
        await _manager.ScarceProducts.Add(product);
        await _manager.Save();
        return new CreatedResponse<ScarceProductDTO>(product.ToDTO());
    }

    public async Task<BaseResponse> Update(Guid id, ScarceProductCreateDTO schema)
    {
        ScarceProduct? product = await _manager.ScarceProducts.GetById(id);
        if(product is null) return new NotFoundResponse(id, nameof(ScarceProduct));
        product.Update(schema);
        _manager.ScarceProducts.Update(product);
        await _manager.Save();
        return new CreatedResponse<ScarceProductDTO>(product.ToDTO());
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
