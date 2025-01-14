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
        OrderByDescending = order => order.CreatedAt;
    }

    public IncomingOrderWithProviderSpecification(DateOnly? from, DateOnly? to) : base(
        obj =>
        DateOnly.FromDateTime(obj.CreatedAt.Date) >= from &&
        DateOnly.FromDateTime(obj.CreatedAt.Date) <= to
    )
    {
        Selector = order => new IncomingOrderDTO
        {
            Id = order.Id,
            CreatedAt = order.CreatedAt,
            ProviderName = order.Provider!.Name,
            Price = order.Price,
            Paid = order.Paid,
        };
        OrderByDescending = order => order.CreatedAt;
    }
}
