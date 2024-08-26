using Pharmacy.Domain.Modules.Orders.Models;
using Pharmacy.Shared.Modules.Orders.DTOs;

namespace Pharmacy.Application.Modules.Orders.Mappers;



public static class PaymentMapper
{
    public static Payment ToModel(this PaymentCreateDTO paymentDTO, Guid customerId) =>
        new()
        {
            Paid = (double)paymentDTO.AmountPaid,
            CustomerId = customerId
        };

    public static PaymentDTO ToDTO(this Payment payment) =>
        new()
        {
            Id = payment.Id,
            AmountPaid = (decimal)payment.Paid,
            CreatedAt = payment.CreatedAt
        };
}