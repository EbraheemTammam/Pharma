using Pharmacy.Application.Mappers;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Utilities;
using Microsoft.AspNetCore.Http;

namespace Pharmacy.Application.Services;

public class ScarceProductService : IScarceProductService
{
    private readonly IRepository<ScarceProduct> _scarceProducts;

    public ScarceProductService(IRepository<ScarceProduct> repo) =>
        _scarceProducts = repo;

    public async Task<Result<IEnumerable<ScarceProductDTO>>> GetAll() =>
        Result.Success(
            (await _scarceProducts.GetAll())
            .ConvertAll(ScarceProductMapper.ToDTO)
        );

    public async Task<Result<ScarceProductDTO>> GetById(Guid id)
    {
        ScarceProduct? product = await _scarceProducts.GetById(id);
        return product switch
        {
            null => Result.Fail<ScarceProductDTO>(AppResponses.NotFoundResponse(id, nameof(ScarceProduct))),
            _ => Result.Success(product.ToDTO())
        };
    }

    public async Task<Result<ScarceProductDTO>> Create(ScarceProductCreateDTO schema)
    {
        ScarceProduct product = schema.ToModel();
        await _scarceProducts.Add(product);
        await _scarceProducts.Save();
        return Result.Success(product.ToDTO(), StatusCodes.Status201Created);
    }

    public async Task<Result<ScarceProductDTO>> Update(Guid id, ScarceProductCreateDTO schema)
    {
        ScarceProduct? product = await _scarceProducts.GetById(id);
        if(product is null) return Result.Fail<ScarceProductDTO>(AppResponses.NotFoundResponse(id, nameof(ScarceProduct)));
        product.Update(schema);
        _scarceProducts.Update(product);
        await _scarceProducts.Save();
        return Result.Success(product.ToDTO(), StatusCodes.Status201Created);
    }

    public async Task<Result> Delete(Guid id)
    {
        ScarceProduct? product = await _scarceProducts.GetById(id);
        if(product is null) return Result.Fail(AppResponses.NotFoundResponse(id, nameof(ScarceProduct)));
        _scarceProducts.Delete(product);
        await _scarceProducts.Save();
        return Result.Success(StatusCodes.Status204NoContent);
    }
}
