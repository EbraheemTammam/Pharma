using Microsoft.AspNetCore.Http;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Mappers;
using Pharmacy.Application.Queries;
using Pharmacy.Application.Responses;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Specifications;

namespace Pharmacy.Application.Utilities;

public static class InternalEventHandler
{
    private static async Task<Result> OrderItemPreSave(IRepositoryManager manager, OrderItem item, Product product)
    {
        product.OwnedElements -= item.Amount;
        product.IsLack = product.OwnedElements <= product.OwnedElements;

        manager.Products.Update(product);

        foreach (ProductItem pItem in await manager.ProductItems.GetAll())
        {
            (pItem.NumberOfElements, item.Amount) =
            (
                pItem.NumberOfElements > item.Amount ?
                (pItem.NumberOfElements - item.Amount, 0) :
                (0, item.Amount - pItem.NumberOfElements)
            );

            manager.ProductItems.Update(pItem);
            if(item.Amount == 0) break;
        }

        return Result.Success();
    }

    public static async Task<Result> OrderItemPreDelete(IRepositoryManager manager, OrderItem item)
    {
        item.Product!.OwnedElements += item.Amount;
        item.Product.IsLack = item.Product.OwnedElements > item.Product.Minimum;
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
        return Result.Success();
    }

    public static async Task<Result<OrderDTO>> OrderPreSave(IRepositoryManager manager, Order order, OrderUpdateDTO orderDTO)
    {
        foreach(OrderItemCreateDTO itemDTO in orderDTO.Items)
        {
            Product? product = await manager.Products.GetById(itemDTO.ProductId);

            if(product is null)
                return Result.Fail<OrderDTO>(AppResponses.NotFoundResponse(itemDTO.ProductId, nameof(Product)));
            if(product.OwnedElements < itemDTO.Amount)
                return Result.Fail<OrderDTO>(AppResponses.BadRequestResponse($"No enough items of {product.Name}, only {product.OwnedElements} remaining"));

            OrderItem oItem = await manager.OrderItems.Add(itemDTO.ToModel(order.Id, product));
            order.TotalPrice += oItem.Amount * product.PricePerElement;

            await OrderItemPreSave(manager, oItem, product);
            manager.Orders.Update(order);
        }

        order.Paid = (double?) orderDTO.Paid ?? order.TotalPrice;
        manager.Orders.Update(order);

        return Result.Success(order.ToDTO(), StatusCodes.Status201Created);
    }

    public static async Task<Result<OrderDTO>> OrderPreUpdate(IRepositoryManager manager, Order order, OrderUpdateDTO orderDTO)
    {
        foreach (OrderItem item in await manager.OrderItems.GetAll(new OrderItemWithProduct(order.Id)))
            await OrderItemPreDelete(manager, item);

        order.TotalPrice = 0;
        return await OrderPreSave(manager, order, orderDTO);
    }

    public static async Task<Result> OrderPreDelete(IRepositoryManager manager, Order order)
    {
        foreach (OrderItem item in await manager.OrderItems.GetAll(new OrderItemWithProduct(order.Id)))
            await OrderItemPreDelete(manager, item);

        return Result.Success();
    }

    public static async Task<Result<IncomingOrderDTO>> IncomingOrderPreSave(IRepositoryManager manager, IEnumerable<ProductItemCreateDTO> items, IncomingOrder incomingOrder)
    {
        foreach(ProductItemCreateDTO itemDTO in items)
        {
            Product? product = await manager.Products.GetById(itemDTO.ProductId);
            if(product is null)
                return Result.Fail<IncomingOrderDTO>(AppResponses.NotFoundResponse(itemDTO.ProductId, nameof(Product)));

            await manager.ProductItems.Add(itemDTO.ToModel(product, incomingOrder.Id));
            product.OwnedElements += itemDTO.NumberOfBoxes * product.NumberOfElements;
            manager.Products.Update(product);
        }
        return Result.Success(incomingOrder.ToDTO(), StatusCodes.Status201Created);
    }

    public static async Task IncomingOrderPreDelete(IRepositoryManager manager, Guid id)
    {
        IEnumerable<ProductItem> items = await manager.ProductItems.GetAll(
            new Specification<ProductItem>(obj => obj.IncomingOrderId == id)
        );
        foreach (ProductItem item in items)
        {
            item.Product!.OwnedElements -= item.NumberOfBoxes * item.Product.NumberOfElements;
            manager.Products.Update(item.Product);
            manager.ProductItems.Delete(item);
        }
    }
}
