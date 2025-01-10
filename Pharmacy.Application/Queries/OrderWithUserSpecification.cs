using Pharmacy.Application.DTOs;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Specifications;

namespace Pharmacy.Application.Queries;

public class OrderWithUserSpecification : Specification<Order, OrderDTO>
{
    public OrderWithUserSpecification()
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

    public OrderWithUserSpecification(Guid orderId) : base(obj => obj.Id == orderId)
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
