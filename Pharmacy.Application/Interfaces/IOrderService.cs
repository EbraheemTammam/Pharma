using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Interfaces;


public interface IOrderService
{
    Task<Result<IEnumerable<OrderDTO>>> GetAll();
    Task<Result<IEnumerable<OrderItemDTO>>> GetItems(Guid id);
    Task<Result<OrderDTO>> GetById(Guid id);
    Task<Result<OrderDTO>> Create(OrderCreateDTO schema);
    Task<Result<OrderDTO>> Update(Guid id, OrderUpdateDTO schema);
    Task<Result> Delete(Guid id);
}
