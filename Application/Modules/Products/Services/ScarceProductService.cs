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
    private readonly IRepositoryManager _repositoryManager;

    public ScarceProductService(IRepositoryManager repoManager) =>
        _repositoryManager = repoManager;

    public async Task<BaseResponse> GetAll() =>
        new OkResponse<IEnumerable<ScarceProductDTO>>(
            (await _repositoryManager.ScarceProducts.GetAll()).ConvertAll(obj => obj.ToDTO())
        );

    public async Task<BaseResponse> GetById(Guid id)
    {
        ScarceProduct? product = await _repositoryManager.ScarceProducts.GetById(id);
        return product is null ? new NotFoundResponse(id, nameof(ScarceProduct))
        : new OkResponse<ScarceProductDTO>(product.ToDTO());
    }

    public async Task<BaseResponse> Create(ScarceProductCreateDTO schema)
    {
        ScarceProduct product = schema.ToModel();
        await _repositoryManager.ScarceProducts.Add(product);
        await _repositoryManager.Save();
        return new OkResponse<ScarceProductDTO>(product.ToDTO());
    }

    public async Task<BaseResponse> Update(Guid id, ScarceProductCreateDTO schema)
    {
        ScarceProduct? product = await _repositoryManager.ScarceProducts.GetById(id);
        if(product is null) return new NotFoundResponse(id, nameof(ScarceProduct));
        product.Update(schema);
        _repositoryManager.ScarceProducts.Update(product);
        await _repositoryManager.Save();
        return new OkResponse<ScarceProductDTO>(product.ToDTO());
    }

    public async Task<BaseResponse> Delete(Guid id)
    {
        ScarceProduct? product = await _repositoryManager.ScarceProducts.GetById(id);
        if(product is null) return new NotFoundResponse(id, nameof(Product));
        _repositoryManager.ScarceProducts.Delete(product);
        await _repositoryManager.Save();
        return new NoContentResponse();
    }
}
