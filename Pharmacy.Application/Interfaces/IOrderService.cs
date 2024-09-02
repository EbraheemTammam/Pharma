using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Interfaces;


public interface IOrderService : IService<Guid>
{
    Task<BaseResponse> GetItems(Guid id);
    Task<BaseResponse> Create(OrderCreateDTO schema, string userName);
    Task<BaseResponse> Update(Guid id, OrderUpdateDTO schema, string userName);
}
