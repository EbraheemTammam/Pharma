using System.Diagnostics.CodeAnalysis;

namespace Pharmacy.Application.DTOs;



public abstract record OrderBaseDTO
{
    [AllowNull]
    public decimal? Paid {get; set;}
}
