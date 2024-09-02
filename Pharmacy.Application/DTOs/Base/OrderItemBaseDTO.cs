using System.ComponentModel.DataAnnotations;
using Pharmacy.Application.Validations;

namespace Pharmacy.Application.DTOs;


public abstract record OrderItemBaseDTO
{
    [Required, NonNegative]
    public int Amount {get; set;}
}
