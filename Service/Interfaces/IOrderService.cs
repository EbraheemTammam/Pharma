using Pharmacy.Shared.Responses;
using Pharmacy.Shared.DTOs;

namespace Pharmacy.Service.Interfaces;


public interface IOrderService : IService<Guid>
{
    Task<BaseResponse> GetItems(Guid id);
    Task<BaseResponse> Create(OrderCreateDTO schema, int userId);
    Task<BaseResponse> Update(Guid id, OrderUpdateDTO schema, int userId);
}