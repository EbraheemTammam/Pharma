using Pharmacy.Services.Modules.Products;
using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Shared.Modules.Products.DTOs;
using Pharmacy.Application.Utilities;
using Pharmacy.Application.Modules.Products.Mappers;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Shared.Generics;
namespace Pharmacy.Application.Modules.Products.Services;



public class IncomingOrderService : IIncomingOrderService
{
    private readonly IRepositoryManager _repositoryManager;

    private async Task<(Product? product, string fieldVal, string fieldType)> _getProduct(Guid? productId, string? barcode)
    {
        if(productId != null)
            return (await _repositoryManager.Products.GetById(productId), productId.ToString(), "id")!;
        return (await _repositoryManager.Products.GetByBarcode(barcode!), barcode, "barcode")!;
    }

    private async Task<BaseResponse> _addItems(IEnumerable<ProductItemCreateDTO> items, Guid incomingOrderId)
    {
        foreach(ProductItemCreateDTO itemDTO in items)
        {
            //  Check if both Id and Barcode are null
            if(itemDTO.ProductId is null && itemDTO.Barcode is null)
                return new BadRequestResponse("product Id or Barcode must be provided");
            //  Get Product
            var result = await _getProduct(itemDTO.ProductId, itemDTO.Barcode);
            //  Check if null
            Product? product = result.product;
            if(product is null)
                return new NotFoundResponse(result.fieldVal, nameof(Product), result.fieldType);
            //  Add Item and  Update Product
            await _repositoryManager.ProductItems.Add(itemDTO.ToModel(product, incomingOrderId));
            product.OwnedElements += itemDTO.NumberOfBoxes * product.NumberOfElements;
            _repositoryManager.Products.Update(product);
        }
        await _repositoryManager.Save();
        return new OkResponse<bool>(true);
    }

    private async Task _preDelete(Guid id)
    {
        IEnumerable<ProductItem> items = await _repositoryManager.ProductItems.Filter(obj => obj.IncomingOrderId == id);
        foreach (ProductItem item in items)
        {
            item.Product!.OwnedElements -= item.NumberOfBoxes * item.Product.NumberOfElements;
            _repositoryManager.Products.Update(item.Product);
            _repositoryManager.ProductItems.Delete(item);
        }
        await _repositoryManager.Save();
    }

    public IncomingOrderService(IRepositoryManager repoManager) =>
        _repositoryManager = repoManager;

    public async Task<BaseResponse> GetAll() =>
        new OkResponse<IEnumerable<IncomingOrderDTO>>(
            (await _repositoryManager.IncomingOrders.GetAll()).ConvertAll(obj => obj.ToDTO())
        );

    public async Task<BaseResponse> GetById(Guid id)
    {
        // Check if Incoming Order Exist
        IncomingOrder? incomingOrder = await _repositoryManager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return new NotFoundResponse(id, nameof(IncomingOrder));
        //  Get Product Provider
        ProductProvider provider = (await _repositoryManager.ProductProviders.GetById(incomingOrder.ProviderId))!;
        return new OkResponse<IncomingOrderDTO>(incomingOrder.ToDTO(provider.Name));
    }

    public async Task<BaseResponse> GetItems(Guid id)
    {
        // Check if Incoming Order Exist
        IncomingOrder? incomingOrder = await _repositoryManager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return new NotFoundResponse(id, nameof(IncomingOrder));
        // Get Items
        IEnumerable<ProductItem> items = await _repositoryManager.ProductItems.Filter(obj => obj.IncomingOrderId == id);
        return new OkResponse<IEnumerable<ProductItemDTO>>(items.ConvertAll(obj => obj.ToDTO()));
    }

    public async Task<BaseResponse> Create(IncomingOrderCreateDTO schema)
    {
        //  Check if Provider Exists
        ProductProvider? provider = await _repositoryManager.ProductProviders.GetById(schema.ProviderId);
        if(provider is null) return new NotFoundResponse(schema.ProviderId, nameof(ProductProvider));
        //  Create Object
        IncomingOrder incomingOrder = await _repositoryManager.IncomingOrders.Add(schema.ToModel());
        //  Add Items
        BaseResponse response = await _addItems(schema.ProductItems, incomingOrder.Id);
        //  Check Successful Addition
        if(response.StatusCode != 200)
            return response;
        //  Save
        await _repositoryManager.Save();
        return new OkResponse<IncomingOrderDTO>(
            incomingOrder.ToDTO(provider.Name)
        );
    }

    public async Task<BaseResponse> Update(Guid id, IncomingOrderUpdateDTO schema)
    {
        //  Check if Incoming Order Exists
        IncomingOrder? incomingOrder = await _repositoryManager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return new NotFoundResponse(id, nameof(IncomingOrder));
        //  Update Object
        incomingOrder.Update(schema);
        _repositoryManager.IncomingOrders.Update(incomingOrder);
        // Save
        await _repositoryManager.Save();
        // Get Provider
        ProductProvider provider = (await _repositoryManager.ProductProviders.GetById(incomingOrder.ProviderId))!;
        return new OkResponse<IncomingOrderDTO>(
            incomingOrder.ToDTO(provider.Name)
        );
    }

    public async Task<BaseResponse> Delete(Guid id)
    {
        //  Check if Incoming Order Exist
        IncomingOrder? incomingOrder = await _repositoryManager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return new NotFoundResponse(id, nameof(IncomingOrder));
        //  Remove Items from Owned Elements
        await _preDelete(id);
        //  Delete
        _repositoryManager.IncomingOrders.Delete(incomingOrder);
        await _repositoryManager.Save();
        return new NoContentResponse();
    }
}
