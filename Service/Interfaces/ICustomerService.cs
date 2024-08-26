using Pharmacy.Shared.Responses;
using Pharmacy.Shared.DTOs;

namespace Pharmacy.Service.Interfaces;



public interface ICustomerService : IService<Guid>
{
    Task<BaseResponse> Create(CustomerCreateDTO customerDTO);
    Task<BaseResponse> Update(Guid id, CustomerCreateDTO customerDTO);
    Task<BaseResponse> GetPaymentOperations(Guid customerId);
    Task<BaseResponse> AddPaymentOperation(Guid customerId, PaymentCreateDTO paymentDTO);
    Task<BaseResponse> RemovePaymentOperation(Guid customerId, int PaymentId);
}
