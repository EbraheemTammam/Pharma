using Microsoft.AspNetCore.Http;
using Pharmacy.Application.Mappers;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Utilities;
using Pharmacy.Application.Queries;

namespace Pharmacy.Application.Services;

public class IncomingOrderService : IIncomingOrderService
{
    private readonly IRepositoryManager _manager;

    public IncomingOrderService(IRepositoryManager repoManager) =>
        _manager = repoManager;

    public async Task<Result<IEnumerable<IncomingOrderDTO>>> GetAll(DateOnly? from, DateOnly? to)
    {
        DateOnly todaysDate = DateOnly.FromDateTime(DateTime.Today.Date);
        return Result.Success
        (
            await _manager.IncomingOrders.GetAll(new IncomingOrderWithProviderSpecification(from ?? new DateOnly(todaysDate.Year, todaysDate.Month, 1), to ?? todaysDate))
        );
    }

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

        IEnumerable<ProductItemDTO> items = await _manager.ProductItems.GetAll(
            new ProductItemWithProductNameSpecification(id)
        );
        return Result.Success(items);
    }

    public async Task<Result<IncomingOrderDTO>> Create(IncomingOrderCreateDTO orderDTO)
    {
        ProductProvider? provider = await _manager.ProductProviders.GetById(orderDTO.ProviderId);
        if(provider is null) return Result.Fail<IncomingOrderDTO>(AppResponses.NotFoundResponse(orderDTO.ProviderId, nameof(ProductProvider)));

        var validitems = await InternalEventHandler.ValidateProductItems(_manager, orderDTO.ProductItems);
        if(!validitems.Succeeded) return Result.Fail<IncomingOrderDTO>(validitems.Response);

        IncomingOrder incomingOrder = await _manager.IncomingOrders.Add(orderDTO.ToModel());

        if(orderDTO.Paid < orderDTO.Price) provider.Indepted += orderDTO.Price - orderDTO.Paid;

        await InternalEventHandler.IncomingOrderPreSave(_manager, orderDTO.ProductItems, incomingOrder);

        await _manager.Save();
        return Result.Success(incomingOrder.ToDTO(provider.Name), StatusCodes.Status201Created);
    }

    public async Task<Result<IncomingOrderDTO>> Update(Guid id, IncomingOrderUpdateDTO schema)
    {
        IncomingOrder? incomingOrder = await _manager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return Result.Fail<IncomingOrderDTO>(AppResponses.NotFoundResponse(id, nameof(IncomingOrder)));

        ProductProvider provider = (await _manager.ProductProviders.GetById(incomingOrder.ProviderId))!;

        if(incomingOrder.Paid < incomingOrder.Price) provider.Indepted -= incomingOrder.Price - incomingOrder.Paid;
        incomingOrder.Update(schema);
        _manager.IncomingOrders.Update(incomingOrder);

        if(schema.Paid < schema.Price) provider.Indepted += schema.Price - schema.Paid;

        await _manager.Save();

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
