using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Interfaces;


public interface IIncomingOrderService : IService<Guid>
{
    Task<BaseResponse> GetItems(Guid id);
    Task<BaseResponse> Create(IncomingOrderCreateDTO schema);
    Task<BaseResponse> Update(Guid id, IncomingOrderUpdateDTO schema);
}
