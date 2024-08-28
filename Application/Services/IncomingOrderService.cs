using Pharmacy.Application.Mappers;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Service.Interfaces;
using Pharmacy.Shared.Responses;
using Pharmacy.Shared.DTOs;
using Pharmacy.Application.Utilities;

namespace Pharmacy.Application.Services;



public class IncomingOrderService : IIncomingOrderService
{
    private readonly IRepositoryManager _manager;

    public IncomingOrderService(IRepositoryManager repoManager) =>
        _manager = repoManager;

    public async Task<BaseResponse> GetAll() =>
        new OkResponse<IEnumerable<IncomingOrderDTO>>(
            (await _manager.IncomingOrders.GetAll()).ConvertAll(obj => obj.ToDTO())
        );

    public async Task<BaseResponse> GetById(Guid id)
    {
        /* ------- Check if Incoming Order Exist ------- */
        IncomingOrder? incomingOrder = await _manager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return new NotFoundResponse(id, nameof(IncomingOrder));
        /* ------- Get Product Provider ------- */
        ProductProvider provider = (await _manager.ProductProviders.GetById(incomingOrder.ProviderId))!;
        return new OkResponse<IncomingOrderDTO>(incomingOrder.ToDTO(provider.Name));
    }

    public async Task<BaseResponse> GetItems(Guid id)
    {
        /* ------- Check if Incoming Order Exist ------- */
        IncomingOrder? incomingOrder = await _manager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return new NotFoundResponse(id, nameof(IncomingOrder));
        /* ------- Get Items ------- */
        IEnumerable<ProductItem> items = await _manager.ProductItems.Filter(obj => obj.IncomingOrderId == id);
        return new OkResponse<IEnumerable<ProductItemDTO>>(items.ConvertAll(obj => obj.ToDTO()));
    }

    public async Task<BaseResponse> Create(IncomingOrderCreateDTO schema)
    {
        /* ------- Check if Provider Exists ------- */
        ProductProvider? provider = await _manager.ProductProviders.GetById(schema.ProviderId);
        if(provider is null) return new NotFoundResponse(schema.ProviderId, nameof(ProductProvider));
        /* ------- Create Object ------- */
        IncomingOrder incomingOrder = await _manager.IncomingOrders.Add(schema.ToModel());
        /* ------- Add Items ------- */
        BaseResponse response = await _manager.AddAllProductItems(schema.ProductItems, incomingOrder.Id);
        /* ------- Check Successful Addition ------- */
        if(response.StatusCode != 200)
            return response;
        /* ------- Save Changes ------- */
        await _manager.Save();
        return new OkResponse<IncomingOrderDTO>(
            incomingOrder.ToDTO(provider.Name)
        );
    }

    public async Task<BaseResponse> Update(Guid id, IncomingOrderUpdateDTO schema)
    {
        /* ------- Check if Incoming Order Exists ------- */
        IncomingOrder? incomingOrder = await _manager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return new NotFoundResponse(id, nameof(IncomingOrder));
        /* ------- Update Object ------- */
        incomingOrder.Update(schema);
        _manager.IncomingOrders.Update(incomingOrder);
        /* ------- Save Changes ------- */
        await _manager.Save();
        /* ------- Get Provider ------- */
        ProductProvider provider = (await _manager.ProductProviders.GetById(incomingOrder.ProviderId))!;
        return new OkResponse<IncomingOrderDTO>(
            incomingOrder.ToDTO(provider.Name)
        );
    }

    public async Task<BaseResponse> Delete(Guid id)
    {
        /* ------- Check if Incoming Order Exist ------- */
        IncomingOrder? incomingOrder = await _manager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return new NotFoundResponse(id, nameof(IncomingOrder));
        /* ------- Remove Items from Owned Elements ------- */
        await _manager.PreDeleteIncomingOrder(id);
        /* ------- Delete ------- */
        _manager.IncomingOrders.Delete(incomingOrder);
        /* ------- Save Changes ------- */
        await _manager.Save();
        /* ------- Return Response ------- */
        return new NoContentResponse();
    }
}
