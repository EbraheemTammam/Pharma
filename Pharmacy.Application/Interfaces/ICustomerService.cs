using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Interfaces;



public interface ICustomerService : IService<Guid>
{
    Task<BaseResponse> Create(CustomerCreateDTO customerDTO);
    Task<BaseResponse> Update(Guid id, CustomerCreateDTO customerDTO);
    Task<BaseResponse> GetPaymentOperations(Guid customerId);
    Task<BaseResponse> AddPaymentOperation(Guid customerId, PaymentCreateDTO paymentDTO);
    Task<BaseResponse> RemovePaymentOperation(Guid customerId, int PaymentId);
}
