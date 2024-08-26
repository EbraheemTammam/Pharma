using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Shared.DTOs;


public class PaymentBaseDTO
{
    [Required]
    public decimal AmountPaid {get; set;}
}

public class PaymentCreateDTO : PaymentBaseDTO
{

}

public class PaymentDTO : PaymentBaseDTO
{
    public int Id {get; set;}
    public DateTime CreatedAt {get; set;}
}
