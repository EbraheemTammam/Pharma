using Pharmacy.Domain.Models;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Mappers;



public static class OrderItemMapper
{
    public static OrderItem ToModel(this OrderItemCreateDTO orderItemDTO, Guid orderId, Product product) =>
        new()
        {
            ProductId = product.Id,
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
            ProductName = orderItem.Product!.Name,
            RemainedItems = orderItem.Product!.OwnedElements
        };

    public static void Update(this OrderItem orderItem, OrderItemCreateDTO orderItemDTO)
    {
        orderItem.Amount = orderItemDTO.Amount;
        orderItem.Price = orderItemDTO.Amount * orderItem.Product!.PricePerElement;
    }
}
