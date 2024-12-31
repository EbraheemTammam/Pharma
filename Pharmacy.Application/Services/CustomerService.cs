using Pharmacy.Application.Mappers;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Interfaces;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Utilities;
using Microsoft.AspNetCore.Http;
using Pharmacy.Domain.Specifications;

namespace Pharmacy.Application.Services;



public class CustomerService : ICustomerService
{
    private readonly IRepository<Customer> _customers;
    private readonly IRepository<Payment> _payments;

    public CustomerService(IRepository<Customer> repo, IRepository<Payment> paymentsRepo) =>
        (_customers, _payments) = (repo, paymentsRepo);

    public async Task<Result<IEnumerable<CustomerDTO>>> GetAll() =>
        Result.Success(
            (await _customers.GetAll())
            .ConvertAll(CustomerMapper.ToDTO)
        );

    public async Task<Result<CustomerDTO>> GetById(Guid id)
    {
        Customer? customer = await _customers.GetById(id);
        return customer switch
        {
            null => Result.Fail<CustomerDTO>(AppResponses.NotFoundResponse(id, nameof(Customer))),
            _ => Result.Success(customer.ToDTO())
        };
    }

    public async Task<Result<CustomerDTO>> Create(CustomerCreateDTO customerDTO)
    {
        Customer customer = customerDTO.ToModel();
        await _customers.Add(customer);
        await _customers.Save();
        return Result.Success(customer.ToDTO(), StatusCodes.Status201Created);
    }

    public async Task<Result<CustomerDTO>> Update(Guid id, CustomerCreateDTO customerDTO)
    {
        Customer? customer = await _customers.GetById(id);
        if(customer is null) return Result.Fail<CustomerDTO>(AppResponses.NotFoundResponse(id, nameof(Customer)));
        customer.Update(customerDTO);
        await _customers.Save();
        return Result.Success(customer.ToDTO(), StatusCodes.Status201Created);
    }

    public async Task<Result> Delete(Guid id)
    {
        Customer? customer = await _customers.GetById(id);
        if(customer is null) return Result.Fail<CustomerDTO>(AppResponses.NotFoundResponse(id, nameof(Customer)));
        _customers.Delete(customer);
        await _customers.Save();
        return Result.Success(StatusCodes.Status204NoContent);
    }

    public async Task<Result<IEnumerable<PaymentDTO>>> GetPaymentOperations(Guid customerId)
    {
        Customer? customer = await _customers.GetById(customerId);
        if(customer is null)
            return Result.Fail<IEnumerable<PaymentDTO>>(AppResponses.NotFoundResponse(customerId, nameof(Customer)));
        return Result.Success(
            (
                await _payments.GetAll(
                    new Specification<Payment>(obj => obj.CustomerId == customerId)
                )
            ).ConvertAll(PaymentMapper.ToDTO)
        );
    }

    public async Task<Result<PaymentDTO>> AddPaymentOperation(Guid customerId, PaymentCreateDTO paymentDTO)
    {
        Customer? customer = await _customers.GetById(customerId);
        if(customer is null) return Result.Fail<PaymentDTO>(AppResponses.NotFoundResponse(customerId, nameof(Customer)));
        Payment operation = paymentDTO.ToModel(customer.Id);
        await _payments.Add(operation);
        customer.Dept -= operation.Paid;
        _customers.Update(customer);
        await _customers.Save();
        await _payments.Save();
        return Result.Success(operation.ToDTO(), StatusCodes.Status201Created);
    }

    public async Task<Result> RemovePaymentOperation(Guid customerId, int paymentId)
    {
        Customer? customer = await _customers.GetById(customerId);
        if(customer is null) return Result.Fail(AppResponses.NotFoundResponse(customerId, nameof(Customer)));
        Payment? operation = await _payments.GetOne(
            new Specification<Payment>(obj => obj.Id == paymentId && obj.CustomerId == customerId)
        );
        if(operation is null)
            return Result.Fail(AppResponses.BadRequestResponse($"Customer has no payment operation with Id: {paymentId}"));
        customer.Dept += operation.Paid;
        _customers.Update(customer);
        _payments.Delete(operation);
        await _customers.Save();
        await _payments.Save();
        return Result.Success(StatusCodes.Status204NoContent);
    }
}
