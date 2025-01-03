using Pharmacy.Domain.Models;
using Pharmacy.Domain.Specifications;

namespace Pharmacy.Application.Queries;

public class OrderItemWithProduct : Specification<OrderItem>
{
    public OrderItemWithProduct(Guid orderId) : base (obj => obj.OrderId == orderId)
    { Includes.Add(obj => obj.Product!); }
}
