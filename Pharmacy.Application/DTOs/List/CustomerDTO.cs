namespace Pharmacy.Application.DTOs;



public record CustomerDTO : CustomerBaseDTO
{
    public Guid Id {get; set;}
}
