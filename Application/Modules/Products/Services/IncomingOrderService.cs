using Pharmacy.Services;
using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Shared.Modules.Products.DTOs;
using Pharmacy.Application.Utilities;
using Pharmacy.Application.Modules.Products.Mappers;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Shared.Generics;
namespace Pharmacy.Application.Services.IncomingOrdersModule;



public class IncomingOrderService : IIncomingOrderService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IProductService _productService;
    private readonly IProductProviderService _productProviderService;

    private BaseResponse _getProduct(Guid? productId, string? barcode)
    {
        BaseResponse response;
        if(productId != null)
            response = _productService.GetById((Guid)productId, false);
        else if (barcode != null)
            response = _productService.GetByBarcode(barcode, false);
        else
            return new BadRequestResponse("Product Id or Barcode must be provided");
        return response;
    }

    private BaseResponse _addItems(IEnumerable<ProductItemCreateDTO> items, Guid incomingOrderId)
    {
        foreach(ProductItemCreateDTO itemDTO in items)
        {
            BaseResponse response = _getProduct(itemDTO.ProductId, itemDTO.Barcode);
            if(!response.Success)
                return response;
            Product product = response.GetResult<Product>();
            _repositoryManager.ProductItems.Add(
                itemDTO.ToModel(product, incomingOrderId)
            );
            product.OwnedElements += itemDTO.NumberOfBoxes * product.NumberOfElements;
            _repositoryManager.Products.Update(product);
        }
        _repositoryManager.Save();
        return new OkResponse<bool>(true);
    }

    private void _preDelete(Guid id)
    {
        IEnumerable<ProductItem> items = GetItems(id, false).GetResult<IEnumerable<ProductItem>>();
        foreach (ProductItem item in items)
        {
            item.Product!.OwnedElements -= item.NumberOfBoxes * item.Product.NumberOfElements;
            _repositoryManager.Products.Update(item.Product);
            _repositoryManager.ProductItems.Delete(item);
        }
        _repositoryManager.Save();
    }

    public IncomingOrderService(
        IRepositoryManager repoManager,
        IProductService productService,
        IProductProviderService productProviderService
    )
    {
        _repositoryManager = repoManager;
        _productService = productService;
        _productProviderService = productProviderService;
    }

    public BaseResponse GetAll() =>
        new OkResponse<IEnumerable<IncomingOrderDTO>>(
            _repositoryManager.IncomingOrders.GetAll().ConvertAll(obj => obj.ToDTO())
        );

    public BaseResponse GetById(Guid id)
    {
        IncomingOrder? incomingOrder = _repositoryManager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return new NotFoundResponse(id, nameof(IncomingOrder));
        BaseResponse response = _productProviderService.GetById(incomingOrder.ProviderId);
        return new OkResponse<IncomingOrderDTO>(
            incomingOrder.ToDTO(response.GetResult<ProductProviderDTO>().Name)
        );
    }

    public BaseResponse GetItems(Guid id, bool AsDTO = true)
    {
        BaseResponse response = GetById(id);
        if(!response.Success) return response;
        IEnumerable<ProductItem> items = _repositoryManager.ProductItems.Filter(obj => obj.IncomingOrderId == id);
        return (
            AsDTO ?
            new OkResponse<IEnumerable<ProductItemDTO>>(items.ConvertAll(obj => obj.ToDTO()))
            : new OkResponse<IEnumerable<ProductItem>>(items)
        );
    }

    public BaseResponse Create(IncomingOrderCreateDTO schema)
    {
        BaseResponse provider_response = _productProviderService.GetById(schema.ProviderId);
        if(!provider_response.Success) return provider_response;
        IncomingOrder incomingOrder = _repositoryManager.IncomingOrders.Add(schema.ToModel());
        BaseResponse response = _addItems(schema.ProductItems, incomingOrder.Id);
        if(!response.Success)
            return response;
        _repositoryManager.Save();
        return new OkResponse<IncomingOrderDTO>(
            incomingOrder.ToDTO(provider_response.GetResult<ProductProviderDTO>().Name)
        );
    }

    public BaseResponse Update(Guid id, IncomingOrderUpdateDTO schema)
    {
        IncomingOrder? incomingOrder = _repositoryManager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return new NotFoundResponse(id, nameof(IncomingOrder));
        incomingOrder.Update(schema);
        incomingOrder = _repositoryManager.IncomingOrders.Update(incomingOrder);
        _repositoryManager.Save();
        BaseResponse provider_response = _productProviderService.GetById(incomingOrder.ProviderId);
        return new OkResponse<IncomingOrderDTO>(
            incomingOrder.ToDTO(provider_response.GetResult<ProductProviderDTO>().Name)
        );
    }

    public BaseResponse Delete(Guid id)
    {
        IncomingOrder? incomingOrder = _repositoryManager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return new NotFoundResponse(id, nameof(IncomingOrder));
        _preDelete(id);
        _repositoryManager.IncomingOrders.Delete(incomingOrder);
        _repositoryManager.Save();
        return new OkResponse<bool>(true);
    }
}
