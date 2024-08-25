using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Pharmacy.Shared.Modules.Orders.DTOs;


public class CustomerBaseDTO
{
    [Required, MaxLength(100)]
    public required string Name {get; set;}

    [AllowNull, DefaultValue(0)]
    public decimal Dept {get; set;}
}

public class CustomerCreateDTO : CustomerBaseDTO
{

}

public class CustomerDTO : CustomerBaseDTO
{
    public Guid Id {get; set;}
}
