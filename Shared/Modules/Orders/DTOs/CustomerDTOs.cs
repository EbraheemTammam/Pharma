using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Pharmacy.Shared.Modules.Orders.DTOs;


public abstract record CustomerBaseDTO
{
    [Required, MaxLength(100)]
    public required string Name {get; set;}

    [AllowNull, DefaultValue(0)]
    public decimal Dept {get; set;}
}

public record CustomerCreateDTO : CustomerBaseDTO
{

}

public record CustomerDTO : CustomerBaseDTO
{
    public Guid Id {get; set;}
}
