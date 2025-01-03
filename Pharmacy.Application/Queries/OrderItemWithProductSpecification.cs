using Pharmacy.Domain.Models;
using Pharmacy.Domain.Specifications;

namespace Pharmacy.Application.Queries;

public class OrderItemWithProductSpecification : Specification<OrderItem>
{
    public OrderItemWithProductSpecification(Guid orderId) : base (obj => obj.OrderId == orderId)
    { Includes.Add(obj => obj.Product!); }
}
