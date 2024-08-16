using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Shared.Modules.Products.DTOs;

namespace Pharmacy.Application.Modules.Products.Mappers;



public static class IncomingOrderMapper
{
    public static IncomingOrder ToModel(this IncomingOrderCreateDTO schema) =>
        new()
        {
            Price = schema.Price,
            Paid = schema.Paid,
            ProviderId = schema.ProviderId
        };

    public static IncomingOrderDTO ToDTO(this IncomingOrder model) =>
        new()
        {
            Price = model.Price,
            Paid = model.Paid,
            CreatedAt = model.CreatedAt,
            Provider = model.Provider!.ToDTO(),
        };

    public static IncomingOrder Update(this IncomingOrder incomingOrder, IncomingOrderUpdateDTO schema)
    {
        incomingOrder.Price = schema.Price;
        incomingOrder.Paid = schema.Paid;
        return incomingOrder;
    }
}
