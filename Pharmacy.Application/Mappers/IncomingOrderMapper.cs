using Pharmacy.Domain.Models;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Mappers;

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
            Price = model.Price,
            Paid = model.Paid,
            CreatedAt = model.CreatedAt,
            ProviderName = provider,
        };

    public static void Update(this IncomingOrder incomingOrder, IncomingOrderUpdateDTO schema)
    {
        incomingOrder.Price = (double)schema.Price;
        incomingOrder.Paid = (double)schema.Paid;
    }
}
