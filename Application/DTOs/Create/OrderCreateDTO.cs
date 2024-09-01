using System.Diagnostics.CodeAnalysis;

namespace Pharmacy.Application.DTOs;



public record OrderCreateDTO : OrderUpdateDTO
{
    [AllowNull]
    public Guid? CustomerId {get; set;}
}
