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
        product.IsLack = product.OwnedElements <= product.Minimum;

        manager.Products.Update(product);
        await manager.Products.Save();

        int amount = item.Amount;
        foreach (ProductItem pItem in await manager.ProductItems.GetAll(
            new Specification<ProductItem>(obj => obj.ProductId == item.ProductId)
        ))
        {
            (pItem.NumberOfElements, amount) =
            (
                pItem.NumberOfElements > amount ?
                (pItem.NumberOfElements - amount, 0) :
                (0, amount - pItem.NumberOfElements)
            );

            manager.ProductItems.Update(pItem);
            if(amount == 0) break;
        }
        await manager.ProductItems.Save();

        return Result.Success();
    }

    public static async Task<Result> OrderItemPreDelete(IRepositoryManager manager, OrderItem item)
    {
        item.Product!.OwnedElements += item.Amount;
        item.Product.IsLack = item.Product.OwnedElements <= item.Product.Minimum;
        manager.Products.Update(item.Product);
        await manager.Products.Save();

        ProductItem pItem =
        (
            await manager.ProductItems.GetAll(
                new Specification<ProductItem>(obj => obj.ProductId == item.ProductId)
            )
        ).Last();

        pItem.NumberOfElements += item.Amount;

        manager.ProductItems.Update(pItem);
        await manager.ProductItems.Save();

        manager.OrderItems.Delete(item);
        await manager.OrderItems.Save();

        return Result.Success();
    }

    public static async Task<Result> ValidateOrderItems(IRepositoryManager manager, IEnumerable<OrderItemCreateDTO> items)
    {
        IEnumerable<Guid> productIds = items.Select(item => item.ProductId);

        IEnumerable<Product> products = await manager.Products.GetAll(
            new Specification<Product>(product => productIds.Contains(product.Id))
        );
        var productDictionary = products.ToDictionary(p => p.Id);

        foreach (var itemDTO in items)
        {
            if (!productDictionary.TryGetValue(itemDTO.ProductId, out var product))
                return Result.Fail(AppResponses.NotFoundResponse(itemDTO.ProductId, nameof(Product)));

            if (product.OwnedElements < itemDTO.Amount)
                return Result.Fail(AppResponses.BadRequestResponse($"Not enough items of {product.Name}, only {product.OwnedElements} remaining"));
        }
        return Result.Success();
    }

    public static async Task<Result> OrderPreSave(IRepositoryManager manager, Order order, OrderUpdateDTO orderDTO)
    {
        IEnumerable<Guid> productIds = orderDTO.Items.Select(item => item.ProductId);

        IEnumerable<Product> products = await manager.Products.GetAll(
            new Specification<Product>(product => productIds.Contains(product.Id))
        );
        var productDictionary = products.ToDictionary(p => p.Id);
        foreach(OrderItemCreateDTO itemDTO in orderDTO.Items)
        {

            OrderItem oItem = await manager.OrderItems.Add(itemDTO.ToModel(order.Id, productDictionary[itemDTO.ProductId]));
            order.TotalPrice += oItem.Amount * productDictionary[itemDTO.ProductId].PricePerElement;

            await OrderItemPreSave(manager, oItem, productDictionary[itemDTO.ProductId]);
            manager.Orders.Update(order);
        }

        order.Paid = (double?) orderDTO.Paid ?? order.TotalPrice;
        manager.Orders.Update(order);
        await manager.Orders.Save();

        return Result.Success();
    }

    public static async Task<Result> OrderPreUpdate(IRepositoryManager manager, Order order, OrderUpdateDTO orderDTO)
    {
        foreach (OrderItem item in await manager.OrderItems.GetAll(new OrderItemWithProductSpecification(order.Id)))
            await OrderItemPreDelete(manager, item);

        order.TotalPrice = 0;
        return await OrderPreSave(manager, order, orderDTO);
    }

    public static async Task<Result> OrderPreDelete(IRepositoryManager manager, Order order)
    {
        foreach (OrderItem item in await manager.OrderItems.GetAll(new OrderItemWithProductSpecification(order.Id)))
            await OrderItemPreDelete(manager, item);

        return Result.Success();
    }

    public static async Task<Result> ValidateProductItems(IRepositoryManager manager, IEnumerable<ProductItemCreateDTO> items)
    {
        IEnumerable<Guid> productIds = items.Select(item => item.ProductId);

        IEnumerable<Product> products = await manager.Products.GetAll(
            new Specification<Product>(product => productIds.Contains(product.Id))
        );
        var productDictionary = products.ToDictionary(p => p.Id);

        foreach (var itemDTO in items)
            if (!productDictionary.TryGetValue(itemDTO.ProductId, out var product))
                return Result.Fail(AppResponses.NotFoundResponse(itemDTO.ProductId, nameof(Product)));
        return Result.Success();
    }

    public static async Task<Result> IncomingOrderPreSave(IRepositoryManager manager, IEnumerable<ProductItemCreateDTO> items, IncomingOrder incomingOrder)
    {
        IEnumerable<Guid> productIds = items.Select(item => item.ProductId);

        IEnumerable<Product> products = await manager.Products.GetAll(
            new Specification<Product>(product => productIds.Contains(product.Id))
        );
        var productDictionary = products.ToDictionary(p => p.Id);

        foreach(ProductItemCreateDTO itemDTO in items)
        {
            await manager.ProductItems.Add(itemDTO.ToModel(productDictionary[itemDTO.ProductId], incomingOrder.Id));
            productDictionary[itemDTO.ProductId].OwnedElements += itemDTO.NumberOfBoxes * productDictionary[itemDTO.ProductId].NumberOfElements;
            productDictionary[itemDTO.ProductId].IsLack = productDictionary[itemDTO.ProductId].NumberOfElements > productDictionary[itemDTO.ProductId].Minimum;
            manager.Products.Update(productDictionary[itemDTO.ProductId]);
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
