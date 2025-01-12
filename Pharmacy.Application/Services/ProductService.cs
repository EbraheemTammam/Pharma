using Pharmacy.Application.Mappers;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Utilities;
using Pharmacy.Domain.Specifications;
using Microsoft.AspNetCore.Http;
using Pharmacy.Application.Queries;

namespace Pharmacy.Application.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _products;
    private readonly IRepository<ProductItem> _productItems;

    public ProductService(IRepository<Product> repo, IRepository<ProductItem> itemsRepo) =>
        (_products, _productItems) = (repo, itemsRepo);

    public async Task<Result<IEnumerable<ProductDTO>>> GetAll() =>
        Result.Success(
            (await _products.GetAll())
            .ConvertAll(ProductMapper.ToDTO)
        );

    public async Task<Result<IEnumerable<ProductDTO>>> GetLacked() =>
        Result.Success(
            (await _products.GetAll(new Specification<Product>(obj => obj.IsLack)))
            .ConvertAll(ProductMapper.ToDTO)
        );

    public async Task<Result<IEnumerable<ProductItemDTO>>> GetAboutToExpire()
    {
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.UtcNow);
        return Result.Success(
            await _productItems.GetAll(
                new ProductItemWithProductNameSpecification(
                    obj =>
                    (((obj.ExpirationDate.Year - currentDate.Year) * 12) + (obj.ExpirationDate.Month - currentDate.Month) <= 6)
                    && (obj.NumberOfElements > 0)
                )
            )
        );
    }

    public async Task<Result<ProductDTO>> GetById(Guid id)
    {
        Product? product = await _products.GetById(id);
        return product switch
        {
            null => Result.Fail<ProductDTO>(AppResponses.NotFoundResponse(id, nameof(Product))),
            _ => Result.Success(product.ToDTO())
        };
    }

    public async Task<Result<ProductDTO>> Create(ProductCreateDTO schema)
    {
        if (await _products.GetOne(new Specification<Product>(obj => obj.Barcode == schema.Barcode)) != null)
            return Result.Fail<ProductDTO>(AppResponses.BadRequestResponse("Product with this barcode already exists"));
        Product product = schema.ToModel();
        product.OwnedElements = 0;
        await _products.Add(product);
        await _products.Save();
        return Result.Success(product.ToDTO(), StatusCodes.Status201Created);
    }

    public async Task<Result<ProductDTO>> Update(Guid id, ProductUpdateDTO schema)
    {
        if (await _products.GetOne(new Specification<Product>(obj => obj.Barcode == schema.Barcode)) != null)
            return Result.Fail<ProductDTO>(AppResponses.BadRequestResponse("Product with this barcode already exists"));
        Product? product = await _products.GetById(id);
        if(product is null) return Result.Fail<ProductDTO>(AppResponses.NotFoundResponse(id, nameof(Product)));
        product.Update(schema);
        _products.Update(product);
        await _products.Save();
        return Result.Success(product.ToDTO(), StatusCodes.Status201Created);
    }

    public async Task<Result> Delete(Guid id)
    {
        Product? product = await _products.GetById(id);
        if(product is null) return Result.Fail(AppResponses.NotFoundResponse(id, nameof(Product)));
        _products.Delete(product);
        await _products.Save();
        return Result.Success(StatusCodes.Status204NoContent);
    }
}
