using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Application.DTOs;



public record UserDTO : UserBaseDTO
{
    public int Id {get; set;}
}
