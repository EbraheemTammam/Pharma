using Microsoft.AspNetCore.Http;
using Pharmacy.Application.Mappers;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Utilities;
using Pharmacy.Domain.Specifications;

namespace Pharmacy.Application.Services;

public class ProductProviderService : IProductProviderService
{
    private readonly IRepository<ProductProvider> _productProviders;
    private readonly IRepository<IncomingOrder> _incomingOrders;

    public ProductProviderService(IRepository<ProductProvider> repo, IRepository<IncomingOrder> repo2) =>
        (_productProviders, _incomingOrders) = (repo, repo2);

    public async Task<Result<IEnumerable<ProductProviderDTO>>> GetAll()
    {
        // this try catch block is needed only once, because of the bug in the database
        try
        {
            return Result.Success(
                (await _productProviders.GetAll())
                .ConvertAll(ProductProviderMapper.ToDTO)
            );
        }
        catch
        {
            var productProviders = await _productProviders.GetAll();
            foreach (var provider in productProviders)
            {
                var orders = await _incomingOrders.GetAll(new Specification<IncomingOrder>(obj => obj.ProviderId == provider.Id));
                provider.Indepted = orders.Sum(obj => obj.Price - obj.Paid);
                _productProviders.Update(provider);
            }
            await _productProviders.Save();
            return Result.Success(
                productProviders.ConvertAll(ProductProviderMapper.ToDTO)
            );
        }
    }

    public async Task<Result<ProductProviderDTO>> GetById(Guid id)
    {
        ProductProvider? productProvider = await _productProviders.GetById(id);
        return productProvider switch
        {
            null => Result.Fail<ProductProviderDTO>(AppResponses.NotFoundResponse(id, nameof(ProductProvider))),
            _ => Result.Success(productProvider.ToDTO())
        };
    }

    public async Task<Result<ProductProviderDTO>> Create(ProductProviderCreateDTO schema)
    {
        ProductProvider productProvider = await _productProviders.Add(schema.ToModel());
        await _productProviders.Save();
        return Result.Success(productProvider.ToDTO(), StatusCodes.Status201Created);
    }

    public async Task<Result<ProductProviderDTO>> Update(Guid id, ProductProviderCreateDTO schema)
    {
        ProductProvider? productProvider = await _productProviders.GetById(id);
        if(productProvider is null) return Result.Fail<ProductProviderDTO>(AppResponses.NotFoundResponse(id, nameof(ProductProvider)));
        productProvider.Update(schema);
        productProvider = _productProviders.Update(productProvider);
        await _productProviders.Save();
        return Result.Success(productProvider.ToDTO());
    }

    public async Task<Result> Delete(Guid id)
    {
        ProductProvider? productProvider = await _productProviders.GetById(id);
        if(productProvider is null) return Result.Fail(AppResponses.NotFoundResponse(id, nameof(ProductProvider)));
        _productProviders.Delete(productProvider);
        await _productProviders.Save();
        return Result.Success(StatusCodes.Status204NoContent);
    }
}
