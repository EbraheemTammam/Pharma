namespace Pharmacy.Application.DTOs;



public record IncomingOrderDTO : IncomingOrderBaseDTO
{
    public Guid Id {get; set;}
    public DateTime CreatedAt {get; set;}
    public required string ProviderName {get; set;}
}
