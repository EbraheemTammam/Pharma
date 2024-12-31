using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Interfaces;



public interface ICustomerService
{
    Task<Result<IEnumerable<CustomerDTO>>> GetAll();
    Task<Result<IEnumerable<PaymentDTO>>> GetPaymentOperations(Guid customerId);
    Task<Result<CustomerDTO>> GetById(Guid id);
    Task<Result<CustomerDTO>> Create(CustomerCreateDTO customerDTO);
    Task<Result<CustomerDTO>> Update(Guid id, CustomerCreateDTO customerDTO);
    Task<Result<PaymentDTO>> AddPaymentOperation(Guid customerId, PaymentCreateDTO paymentDTO);
    Task<Result> Delete(Guid id);
    Task<Result> RemovePaymentOperation(Guid customerId, int PaymentId);
}
