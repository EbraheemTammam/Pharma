using Pharmacy.Application.Modules.Orders.Mappers;
using Pharmacy.Application.Utilities;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Modules.Orders.Models;
using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Orders.DTOs;

namespace Pharmacy.Application.Modules.Orders.Services;



public class CustomerService
{
    private readonly IRepositoryManager _manager;
    public CustomerService(IRepositoryManager manager) =>
        _manager = manager;

    public async Task<BaseResponse> GetAll() =>
        new OkResponse<IEnumerable<CustomerDTO>>
        (
            (await _manager.Customers.GetAll()).ConvertAll(obj => obj.ToDTO())
        );

    public async Task<BaseResponse> GetById(Guid id)
    {
        Customer? customer = await _manager.Customers.GetById(id);
        return
        (
            customer is null ? new NotFoundResponse(id, nameof(Customer))
            : new OkResponse<CustomerDTO>(customer.ToDTO())
        );
    }

    public async Task<BaseResponse> Create(CustomerCreateDTO customerDTO)
    {
        Customer customer = customerDTO.ToModel();
        await _manager.Customers.Add(customer);
        await _manager.Save();
        return new OkResponse<CustomerDTO>(customer.ToDTO());
    }

    public async Task<BaseResponse> Update(Guid id, CustomerCreateDTO customerDTO)
    {
        Customer? customer = await _manager.Customers.GetById(id);
        if(customer is null) return new NotFoundResponse(id, nameof(Customer));
        customer.Update(customerDTO);
        await _manager.Save();
        return new OkResponse<CustomerDTO>(customer.ToDTO());
    }

    public async Task<BaseResponse> Delete(Guid id)
    {
        Customer? customer = await _manager.Customers.GetById(id);
        if(customer is null) return new NotFoundResponse(id, nameof(Customer));
        _manager.Customers.Delete(customer);
        await _manager.Save();
        return new NoContentResponse();
    }
}
