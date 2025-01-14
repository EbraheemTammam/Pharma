using Pharmacy.Domain.Models;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Mappers;

public static class OrderMapper
{
    public static Order ToModel(this OrderCreateDTO orderDTO, int userId) =>
        new()
        {
            TotalPrice = 0,
            UserId = userId,
            CustomerId = orderDTO.CustomerId
        };

    public static OrderDTO ToDTO(this Order order, Guid customerId) =>
        order.ToDTO(null, customerId, order.CreatedBy!.GetFullName());

    public static OrderDTO ToDTO(this Order order, string? customerName, Guid customerId, string userFullName) =>
        new()
        {
            Id = order.Id,
            Paid = (decimal)order.Paid,
            CreatedAt = order.CreatedAt,
            TotalPrice = (decimal)order.TotalPrice,
            CustomerName = customerName,
            CustomerId = customerId,
            CreatedBy = userFullName
        };
}
