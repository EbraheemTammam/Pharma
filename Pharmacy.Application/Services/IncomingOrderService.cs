using Pharmacy.Application.Mappers;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Utilities;
using Pharmacy.Application.Queries;
using Pharmacy.Domain.Specifications;
using Microsoft.AspNetCore.Http;

namespace Pharmacy.Application.Services;

public class IncomingOrderService : IIncomingOrderService
{
    private readonly IRepositoryManager _manager;

    public IncomingOrderService(IRepositoryManager repoManager) =>
        _manager = repoManager;

    public async Task<Result<IEnumerable<IncomingOrderDTO>>> GetAll() =>
        Result.Success
        (
            (await _manager.IncomingOrders.GetAll())
            .ConvertAll(obj => obj.ToDTO())
        );

    public async Task<Result<IncomingOrderDTO>> GetById(Guid id)
    {
        IncomingOrderDTO? incomingOrder = await _manager.IncomingOrders.GetOne(
            new IncomingOrderWithProviderSpecification()
        );
        return incomingOrder switch
        {
            null => Result.Fail<IncomingOrderDTO>(AppResponses.NotFoundResponse(id, nameof(IncomingOrder))),
            _ => Result.Success(incomingOrder)
        };
    }

    public async Task<Result<IEnumerable<ProductItemDTO>>> GetItems(Guid id)
    {
        /* ------- Check if Incoming Order Exist ------- */
        IncomingOrder? incomingOrder = await _manager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return Result.Fail<IEnumerable<ProductItemDTO>>(AppResponses.NotFoundResponse(id, nameof(IncomingOrder)));
        /* ------- Get Items ------- */
        IEnumerable<ProductItem> items = await _manager.ProductItems.GetAll(
            new Specification<ProductItem>(obj => obj.IncomingOrderId == id)
        );
        return Result.Success(items.ConvertAll(obj => obj.ToDTO()));
    }

    public async Task<Result<IncomingOrderDTO>> Create(IncomingOrderCreateDTO schema)
    {
        /* ------- Check if Provider Exists ------- */
        ProductProvider? provider = await _manager.ProductProviders.GetById(schema.ProviderId);
        if(provider is null) return Result.Fail<IncomingOrderDTO>(AppResponses.NotFoundResponse(schema.ProviderId, nameof(ProductProvider)));
        /* ------- Create Object ------- */
        IncomingOrder incomingOrder = await _manager.IncomingOrders.Add(schema.ToModel());
        /* ------- Add Items ------- */
        Result<IncomingOrderDTO> response = await _manager.AddAllProductItems(schema.ProductItems, incomingOrder);
        /* ------- Check Successful Addition ------- */
        if(!response.Succeeded) return response;
        /* ------- Save Changes ------- */
        await _manager.Save();
        return Result.Success(incomingOrder.ToDTO(provider.Name));
    }

    public async Task<Result<IncomingOrderDTO>> Update(Guid id, IncomingOrderUpdateDTO schema)
    {
        /* ------- Check if Incoming Order Exists ------- */
        IncomingOrder? incomingOrder = await _manager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return Result.Fail<IncomingOrderDTO>(AppResponses.NotFoundResponse(id, nameof(IncomingOrder)));
        /* ------- Update Object ------- */
        incomingOrder.Update(schema);
        _manager.IncomingOrders.Update(incomingOrder);
        /* ------- Save Changes ------- */
        await _manager.Save();
        /* ------- Get Provider ------- */
        ProductProvider provider = (await _manager.ProductProviders.GetById(incomingOrder.ProviderId))!;
        return Result.Success(incomingOrder.ToDTO(provider.Name));
    }

    public async Task<Result> Delete(Guid id)
    {
        /* ------- Check if Incoming Order Exist ------- */
        IncomingOrder? incomingOrder = await _manager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return Result.Fail(AppResponses.NotFoundResponse(id, nameof(IncomingOrder)));
        /* ------- Remove Items from Owned Elements ------- */
        await _manager.PreDeleteIncomingOrder(id);
        /* ------- Delete ------- */
        _manager.IncomingOrders.Delete(incomingOrder);
        /* ------- Save Changes ------- */
        await _manager.Save();
        /* ------- Return Response ------- */
        return Result.Success(StatusCodes.Status204NoContent);
    }
}
