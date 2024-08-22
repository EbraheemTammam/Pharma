using Pharmacy.Domain.Modules.Products.Models;
using Pharmacy.Shared.Modules.Products.DTOs;

namespace Pharmacy.Application.Modules.Products.Mappers;



public static class IncomingOrderMapper
{
    public static IncomingOrder ToModel(this IncomingOrderCreateDTO schema) =>
        new()
        {
            Price = (double)schema.Price,
            Paid = (double)schema.Paid,
            ProviderId = schema.ProviderId
        };

    public static IncomingOrderDTO ToDTO(this IncomingOrder model) =>
        model.ToDTO(model.Provider!.Name);

    public static IncomingOrderDTO ToDTO(this IncomingOrder model, string provider) =>
        new()
        {
            Id = model.Id,
            Price = (decimal)model.Price,
            Paid = (decimal)model.Paid,
            CreatedAt = model.CreatedAt,
            ProviderName = provider,
        };

    public static IncomingOrder Update(this IncomingOrder incomingOrder, IncomingOrderUpdateDTO schema)
    {
        incomingOrder.Price = (double)schema.Price;
        incomingOrder.Paid = (double)schema.Paid;
        return incomingOrder;
    }
}
