using Pharmacy.Domain.Models;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Mappers;

public static class OrderItemMapper
{
    public static OrderItem ToModel(this OrderItemCreateDTO orderItemDTO, Guid orderId, Product product) =>
        new()
        {
            ProductId = orderItemDTO.ProductId,
            Amount = orderItemDTO.Amount,
            Price = orderItemDTO.Amount * product.PricePerElement,
            OrderId = orderId
        };

    public static OrderItemDTO ToDTO(this OrderItem orderItem) =>
        new()
        {
            Id = orderItem.Id,
            Amount = orderItem.Amount,
            Price = (decimal)orderItem.Price,
            ProductId = orderItem.Product!.Id,
            ProductName = orderItem.Product!.Name,
            ProductBarcode = orderItem.Product!.Barcode,
            ProductIsLack = orderItem.Product!.IsLack,
            RemainedItems = orderItem.Product!.OwnedElements,
            TotalPrice = (decimal) orderItem.Price * orderItem.Amount
        };

    public static void Update(this OrderItem orderItem, OrderItemCreateDTO orderItemDTO)
    {
        orderItem.Amount = orderItemDTO.Amount;
        orderItem.Price = orderItemDTO.Amount * orderItem.Product!.PricePerElement;
    }
}
