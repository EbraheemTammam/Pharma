using Pharmacy.Application.DTOs;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Specifications;

namespace Pharmacy.Application.Queries;

public class UserOrdersSpecification : Specification<Order, OrderDTO>
{
    public UserOrdersSpecification(int userId, DateOnly? from, DateOnly? to)
    : base(
        obj => obj.UserId == userId &&
        DateOnly.FromDateTime(obj.CreatedAt.Date) >= from &&
        DateOnly.FromDateTime(obj.CreatedAt.Date) <= to
    )
    {
        Selector = order => new OrderDTO
        {
            Id = order.Id,
            Paid = (decimal)order.Paid,
            CreatedAt = order.CreatedAt,
            TotalPrice = (decimal)order.TotalPrice,
            CustomerName = order.Customer!.Name,
            CreatedBy = order.CreatedBy!.GetFullName()
        };
        OrderByDescending = order => order.CreatedAt;
    }
}
