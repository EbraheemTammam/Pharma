using Pharmacy.Domain.Models;
using Pharmacy.Shared.DTOs;

namespace Pharmacy.Application.Mappers;



public static class OrderMapper
{
    public static OrderDTO ToDTO(this Order order) =>
        new()
        {
            Paid = (decimal)order.Paid,
            CreatedAt = order.CreatedAt,
            TotalPrice = (decimal)order.TotalPrice,
            CustomerName = order.Customer!.Name,
            CreatedBy = $"{order.CreatedBy!.FirstName} {order.CreatedBy.LastName}"
        };
}
