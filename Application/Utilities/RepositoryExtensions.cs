using Pharmacy.Application.Mappers;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Models;
using Pharmacy.Shared.DTOs;
using Pharmacy.Shared.Responses;

namespace Pharmacy.Application.Utilities;



internal static class RepositoryExtensions
{
    public static async Task<(Product? product, string fieldVal, string fieldType)> GetByIdOrBarcode(this IProductRepository repo, Guid? id, string? barcode)
    {
        if(id is not null) return (await repo.GetById(id), id.ToString(), "id")!;
        return (await repo.GetByBarcode(barcode!), barcode, "barcode")!;
    }

    public static async Task PreDeleteIncomingOrder(this IRepositoryManager manager, Guid id)
    {
        IEnumerable<ProductItem> items = await manager.ProductItems.Filter(obj => obj.IncomingOrderId == id);
        foreach (ProductItem item in items)
        {
            item.Product!.OwnedElements -= item.NumberOfBoxes * item.Product.NumberOfElements;
            manager.Products.Update(item.Product);
        }
    }

    public static async Task PreDeleteOrder(this IRepositoryManager manager, Guid id)
    {
        IEnumerable<OrderItem> items = await manager.OrderItems.Filter(obj => obj.OrderId == id);
        foreach (OrderItem item in items)
        {
            item.Product!.OwnedElements += item.Amount;
            manager.Products.Update(item.Product);
            ProductItem pItem = (await manager.ProductItems.GetLastByProduct(item.ProductId))!;
            pItem.NumberOfElements += item.Amount;
            manager.ProductItems.Update(pItem);
        }
    }

    public static async Task<BaseResponse> AddAllProductItems(this IRepositoryManager manager, IEnumerable<ProductItemCreateDTO> items, Guid incomingOrderId)
    {
        foreach(ProductItemCreateDTO itemDTO in items)
        {
            //  Get Product
            var result = await manager.Products.GetByIdOrBarcode(itemDTO.ProductId, itemDTO.Barcode);
            //  Check if null
            Product? product = result.product;
            if(product is null)
                return new NotFoundResponse(result.fieldVal, nameof(Product), result.fieldType);
            //  Add Item and  Update Product
            await manager.ProductItems.Add(itemDTO.ToModel(product, incomingOrderId));
            product.OwnedElements += itemDTO.NumberOfBoxes * product.NumberOfElements;
            manager.Products.Update(product);
        }
        return new OkResponse<bool>(true);
    }

    public static async Task<BaseResponse> AddAllOrderItems(this IRepositoryManager manager, IEnumerable<OrderItemCreateDTO> items, Order order)
    {
        foreach(OrderItemCreateDTO itemDTO in items)
        {
            //  Get Product
            var result = await manager.Products.GetByIdOrBarcode(itemDTO.ProductId, itemDTO.ProductBarcode);
            //  Check if null
            Product? product = result.product;
            if(product is null)
                return new NotFoundResponse(result.fieldVal, nameof(Product), result.fieldType);
            //  Add Item and  Update Product
            await manager.OrderItems.Add(itemDTO.ToModel(order.Id, product));
            product.OwnedElements -= itemDTO.Amount;
            manager.Products.Update(product);
            order.TotalPrice += itemDTO.Amount * product.PricePerElement;
            manager.Orders.Update(order);
        }
        return new OkResponse<bool>(true);
    }
}
