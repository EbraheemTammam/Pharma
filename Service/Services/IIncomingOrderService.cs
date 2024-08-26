using Pharmacy.Services.Interfaces;
using Pharmacy.Shared.Generics;
using Pharmacy.Shared.DTOs;

namespace Pharmacy.Service.Services;


public interface IIncomingOrderService : IService<Guid>
{
    Task<BaseResponse> GetItems(Guid id);
    Task<BaseResponse> Create(IncomingOrderCreateDTO schema);
    Task<BaseResponse> Update(Guid id, IncomingOrderUpdateDTO schema);
}
