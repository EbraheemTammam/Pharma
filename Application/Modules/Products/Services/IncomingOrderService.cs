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

    private BaseResponse _getProduct(Guid? productId, string? barcode)
    {
        BaseResponse response;
        if(productId != null)
            response = _productService.GetById((Guid)productId);
        else if (barcode != null)
            response = _productService.GetByBarcode(barcode);
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

    public IncomingOrderService(IRepositoryManager repoManager, IProductService productService)
    {
        _repositoryManager = repoManager;
        _productService = productService;
    }

    public BaseResponse GetAll() =>
        new OkResponse<IEnumerable<IncomingOrderDTO>>(
            _repositoryManager.IncomingOrders.GetAll().ConvertAll(obj => obj.ToDTO())
        );

    public BaseResponse GetById(Guid id)
    {
        IncomingOrder? incomingOrder = _repositoryManager.IncomingOrders.GetById(id);
        return incomingOrder is null ? new NotFoundResponse(id, nameof(IncomingOrder))
        : new OkResponse<IncomingOrderDTO>(incomingOrder.ToDTO());
    }

    public BaseResponse Create(IncomingOrderCreateDTO schema)
    {
        IncomingOrder incomingOrder = _repositoryManager.IncomingOrders.Add(schema.ToModel());
        _repositoryManager.Save();
        BaseResponse response = _addItems(schema.ProductItems, incomingOrder.Id);
        if(!response.Success)
        {
            _repositoryManager.IncomingOrders.Delete(incomingOrder);
            _repositoryManager.Save();
            return response;
        }
        return new OkResponse<IncomingOrderDTO>(incomingOrder.ToDTO());
    }

    public BaseResponse Update(Guid id, IncomingOrderUpdateDTO schema)
    {
        IncomingOrder? incomingOrder = _repositoryManager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return new NotFoundResponse(id, nameof(IncomingOrder));
        incomingOrder.Update(schema);
        incomingOrder = _repositoryManager.IncomingOrders.Update(incomingOrder);
        _repositoryManager.Save();
        return new OkResponse<IncomingOrderDTO>(incomingOrder.ToDTO());
    }

    public BaseResponse Delete(Guid id)
    {
        IncomingOrder? incomingOrder = _repositoryManager.IncomingOrders.GetById(id);
        if(incomingOrder is null) return new NotFoundResponse(id, nameof(IncomingOrder));
        _repositoryManager.IncomingOrders.Delete(incomingOrder);
        _repositoryManager.Save();
        return new OkResponse<bool>(true);
    }
}
