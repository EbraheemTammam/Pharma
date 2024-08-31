namespace Pharmacy.Application.DTOs;



public record PaymentDTO : PaymentBaseDTO
{
    public int Id {get; set;}
    public DateTime CreatedAt {get; set;}
}
