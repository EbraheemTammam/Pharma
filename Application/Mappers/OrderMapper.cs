using Pharmacy.Domain.Models;
using Pharmacy.Shared.DTOs;

namespace Pharmacy.Application.Mappers;



public static class OrderMapper
{
    public static Order ToModel(this OrderCreateDTO orderDTO, int userId) =>
        new()
        {
            Paid = (double)orderDTO.Paid,
            TotalPrice = 0,
            UserId = userId,
            CustomerId = orderDTO.CustomerId
        };

    public static OrderDTO ToDTO(this Order order) =>
        order.ToDTO(order.Customer!.Name, $"{order.CreatedBy!.FirstName} {order.CreatedBy.LastName}");

    public static OrderDTO ToDTO(this Order order, string CustomerName, string userFullName) =>
        new()
        {
            Paid = (decimal)order.Paid,
            CreatedAt = order.CreatedAt,
            TotalPrice = (decimal)order.TotalPrice,
            CustomerName = CustomerName,
            CreatedBy = userFullName
        };
}
