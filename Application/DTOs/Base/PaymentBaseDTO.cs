using System.ComponentModel.DataAnnotations;
using Pharmacy.Application.Validations;

namespace Pharmacy.Application.DTOs;


public class PaymentBaseDTO
{
    [Required, NonNegative]
    public decimal AmountPaid {get; set;}
}
