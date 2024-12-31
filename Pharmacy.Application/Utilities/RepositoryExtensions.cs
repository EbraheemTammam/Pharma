using Pharmacy.Application.Mappers;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Models;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Responses;
using Microsoft.AspNetCore.Http;
using Pharmacy.Domain.Specifications;

namespace Pharmacy.Application.Utilities;



internal static class RepositoryExtensions
{
    public static async Task PreDeleteIncomingOrder(this IRepositoryManager manager, Guid id)
    {
        IEnumerable<ProductItem> items = await manager.ProductItems.GetAll(
            new Specification<ProductItem>(obj => obj.IncomingOrderId == id)
        );
        foreach (ProductItem item in items)
        {
            item.Product!.OwnedElements -= item.NumberOfBoxes * item.Product.NumberOfElements;
            manager.Products.Update(item.Product);
        }
    }

    public static async Task PreDeleteOrder(this IRepositoryManager manager, Guid id)
    {
        IEnumerable<OrderItem> items = await manager.OrderItems.GetAll(
            new Specification<OrderItem>(obj => obj.OrderId == id)
        );
        foreach (OrderItem item in items)
        {
            item.Product!.OwnedElements += item.Amount;
            manager.Products.Update(item.Product);
            ProductItem pItem =
            (
                await manager.ProductItems.GetAll(
                    new Specification<ProductItem>(obj => obj.ProductId == item.ProductId)
                )
            ).Last();
            pItem.NumberOfElements += item.Amount;
            manager.ProductItems.Update(pItem);
        }
    }

    public static async Task<Result<IncomingOrderDTO>> AddAllProductItems(this IRepositoryManager manager, IEnumerable<ProductItemCreateDTO> items, IncomingOrder incomingOrder)
    {
        foreach(ProductItemCreateDTO itemDTO in items)
        {
            /* ------- Get Product ------- */
            Product? product = await manager.Products.GetById(itemDTO.ProductId);
            /* ------- Check if product null ------- */
            if(product is null)
                return Result.Fail<IncomingOrderDTO>(AppResponses.NotFoundResponse(itemDTO.ProductId, nameof(Product)));
            /* ------- Add Item and Update Product ------- */
            await manager.ProductItems.Add(itemDTO.ToModel(product, incomingOrder.Id));
            product.OwnedElements += itemDTO.NumberOfBoxes * product.NumberOfElements;
            manager.Products.Update(product);
        }
        return Result.Success(incomingOrder.ToDTO(), StatusCodes.Status201Created);
    }

    public static async Task<Result<OrderDTO>> AddAllOrderItems(this IRepositoryManager manager, IEnumerable<OrderItemCreateDTO> items, Order order)
    {
        foreach(OrderItemCreateDTO itemDTO in items)
        {
            /* ------- Get Product ------- */
            Product? product = await manager.Products.GetById(itemDTO.ProductId);
            /* ------- Check if product null ------- */
            if(product is null)
                return Result.Fail<OrderDTO>(AppResponses.NotFoundResponse(itemDTO.ProductId, nameof(Product)));
            /* ------- Add Item and Update Product ------- */
            await manager.OrderItems.Add(itemDTO.ToModel(order.Id, product));
            product.OwnedElements -= itemDTO.Amount;
            manager.Products.Update(product);
            /* ------- Update Order ------- */
            order.TotalPrice += itemDTO.Amount * product.PricePerElement;
            manager.Orders.Update(order);
        }
        return Result.Success(order.ToDTO(), StatusCodes.Status201Created);
    }

    public static async Task DeleteAllOrderItems(this IRepositoryManager manager, Guid id)
    {
        IEnumerable<OrderItem> items = await manager.OrderItems.GetAll(
            new Specification<OrderItem>(obj => obj.OrderId == id)
        );
        foreach (OrderItem item in items)
        {
            item.Product!.OwnedElements += item.Amount;
            manager.Products.Update(item.Product);
            ProductItem pItem =
            (
                await manager.ProductItems.GetAll(
                    new Specification<ProductItem>(obj => obj.ProductId == item.ProductId)
                )
            ).Last();
            pItem.NumberOfElements += item.Amount;
            manager.ProductItems.Update(pItem);
            manager.OrderItems.Delete(item);
        }
    }
}
