using System.ComponentModel.DataAnnotations;
using Pharmacy.Application.Validations;

namespace Pharmacy.Application.DTOs;



public abstract record ProductItemBaseDTO
{
    [Required]
    public DateOnly ExpirationDate {get; set;}

    [Required, NonNegative]
    public int NumberOfBoxes {get; set;}
}
