using Pharmacy.Shared.Responses;
using Pharmacy.Shared.DTOs;

namespace Pharmacy.Application.Interfaces;


public interface IIncomingOrderService : IService<Guid>
{
    Task<BaseResponse> GetItems(Guid id);
    Task<BaseResponse> Create(IncomingOrderCreateDTO schema);
    Task<BaseResponse> Update(Guid id, IncomingOrderUpdateDTO schema);
}
