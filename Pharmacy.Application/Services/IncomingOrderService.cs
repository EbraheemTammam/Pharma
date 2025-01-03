using Microsoft.AspNetCore.Http;
using Pharmacy.Application.Mappers;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Utilities;
using Pharmacy.Application.Queries;
using Pharmacy.Domain.Specifications;

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
        IncomingOrder? incomingOrder = await _manager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return Result.Fail<IEnumerable<ProductItemDTO>>(AppResponses.NotFoundResponse(id, nameof(IncomingOrder)));

        IEnumerable<ProductItem> items = await _manager.ProductItems.GetAll(
            new Specification<ProductItem>(obj => obj.IncomingOrderId == id)
        );
        return Result.Success(items.ConvertAll(obj => obj.ToDTO()));
    }

    public async Task<Result<IncomingOrderDTO>> Create(IncomingOrderCreateDTO orderDTO)
    {
        ProductProvider? provider = await _manager.ProductProviders.GetById(orderDTO.ProviderId);
        if(provider is null) return Result.Fail<IncomingOrderDTO>(AppResponses.NotFoundResponse(orderDTO.ProviderId, nameof(ProductProvider)));

        IncomingOrder incomingOrder = await _manager.IncomingOrders.Add(orderDTO.ToModel());

        Result<IncomingOrderDTO> response = await InternalEventHandler.IncomingOrderPreSave(_manager, orderDTO.ProductItems, incomingOrder);
        if(!response.Succeeded) return response;

        await _manager.Save();
        return Result.Success(incomingOrder.ToDTO(provider.Name), StatusCodes.Status201Created);
    }

    public async Task<Result<IncomingOrderDTO>> Update(Guid id, IncomingOrderUpdateDTO schema)
    {
        IncomingOrder? incomingOrder = await _manager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return Result.Fail<IncomingOrderDTO>(AppResponses.NotFoundResponse(id, nameof(IncomingOrder)));

        incomingOrder.Update(schema);
        _manager.IncomingOrders.Update(incomingOrder);

        await _manager.Save();

        ProductProvider provider = (await _manager.ProductProviders.GetById(incomingOrder.ProviderId))!;
        return Result.Success(incomingOrder.ToDTO(provider.Name), StatusCodes.Status201Created);
    }

    public async Task<Result> Delete(Guid id)
    {
        IncomingOrder? incomingOrder = await _manager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return Result.Fail(AppResponses.NotFoundResponse(id, nameof(IncomingOrder)));

        await InternalEventHandler.IncomingOrderPreDelete(_manager, id);
        _manager.IncomingOrders.Delete(incomingOrder);

        await _manager.Save();
        return Result.Success(StatusCodes.Status204NoContent);
    }
}
