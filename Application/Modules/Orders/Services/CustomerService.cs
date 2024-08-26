using Pharmacy.Application.Modules.Orders.Mappers;
using Pharmacy.Application.Utilities;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Domain.Modules.Orders.Models;
using Pharmacy.Services.Modules.Orders;
using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Orders.DTOs;

namespace Pharmacy.Application.Modules.Orders.Services;



public class CustomerService : ICustomerService
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

    public async Task<BaseResponse> GetPaymentOperations(Guid customerId)
    {
        Customer? customer = await _manager.Customers.GetById(customerId);
        if(customer is null) return new NotFoundResponse(customerId, nameof(Customer));
        return new OkResponse<IEnumerable<PaymentDTO>>
        (
            (await _manager.Payments.Filter(obj => obj.CustomerId == customerId))
            .ConvertAll(obj => obj.ToDTO())
        );
    }

    public async Task<BaseResponse> AddPaymentOperation(Guid customerId, PaymentCreateDTO paymentDTO)
    {
        Customer? customer = await _manager.Customers.GetById(customerId);
        if(customer is null) return new NotFoundResponse(customerId, nameof(Customer));
        Payment operation = paymentDTO.ToModel(customer.Id);
        await _manager.Payments.Add(operation);
        await _manager.Save();
        return new OkResponse<PaymentDTO>(operation.ToDTO());
    }

    public async Task<BaseResponse> RemovePaymentOperation(Guid customerId, int paymentId)
    {
        Customer? customer = await _manager.Customers.GetById(customerId);
        if(customer is null) return new NotFoundResponse(customerId, nameof(Customer));
        Payment? operation = await _manager.Payments.GetByIdAndCustomer(paymentId, customerId);
        if(operation is null)
            return new BadRequestResponse($"Customer has no payment operation with Id: {paymentId}");
        return new OkResponse<PaymentDTO>(operation.ToDTO());
    }
}