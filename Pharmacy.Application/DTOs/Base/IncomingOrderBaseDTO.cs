using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Pharmacy.Application.Validations;

namespace Pharmacy.Application.DTOs;



public abstract record IncomingOrderBaseDTO
{
    [Required, NonNegative]
    public double Price {get; set;}

    [Required, NonNegative, DefaultValue(0)]
    public double Paid {get; set;} = 0;
}
