using Pharmacy.Domain.Modules.Orders.Models;
using Pharmacy.Shared.Modules.Orders.DTOs;

namespace Pharmacy.Application.Modules.Orders.Mappers;



public static class CustomerMapper
{
    public static Customer ToModel(this CustomerCreateDTO customerDTO) =>
        new()
        {
            Name = customerDTO.Name,
            Dept = (double)customerDTO.Dept
        };

    public static CustomerDTO ToDTO(this Customer customer) =>
        new()
        {
            Id = customer.Id,
            Name = customer.Name,
            Dept = (decimal)customer.Dept
        };

    public static void Update(this Customer customer, CustomerCreateDTO customerDTO)
    {
        customer.Name = customerDTO.Name;
        customer.Dept = (double)customerDTO.Dept;
    }
}