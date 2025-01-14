using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Interfaces;


public interface IIncomingOrderService
{
    Task<Result<IEnumerable<IncomingOrderDTO>>> GetAll(DateOnly? from, DateOnly? to);
    Task<Result<IEnumerable<ProductItemDTO>>> GetItems(Guid id);
    Task<Result<IncomingOrderDTO>> GetById(Guid id);
    Task<Result<IncomingOrderDTO>> Create(IncomingOrderCreateDTO schema);
    Task<Result<IncomingOrderDTO>> Update(Guid id, IncomingOrderUpdateDTO schema);
    Task<Result> Delete(Guid id);
}
