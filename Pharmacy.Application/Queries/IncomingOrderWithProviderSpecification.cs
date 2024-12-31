using Pharmacy.Application.DTOs;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Specifications;

namespace Pharmacy.Application.Queries;

public class IncomingOrderWithProviderSpecification : Specification<IncomingOrder, IncomingOrderDTO>
{
    public IncomingOrderWithProviderSpecification()
    {
        Selector = order => new IncomingOrderDTO
        {
            Id = order.Id,
            CreatedAt = order.CreatedAt,
            ProviderName = order.Provider!.Name,
            Price = order.Price,
            Paid = order.Paid,
        };
    }
}
