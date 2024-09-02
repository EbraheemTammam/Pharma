using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Pharmacy.Application.Validations;

namespace Pharmacy.Application.DTOs;



public abstract record IncomingOrderBaseDTO
{
    [Required, NonNegative]
    public decimal Price {get; set;}

    [Required, NonNegative, DefaultValue(0)]
    public decimal Paid {get; set;} = 0;
}
