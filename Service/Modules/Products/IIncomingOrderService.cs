using Pharmacy.Services.Interfaces;
using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Products.DTOs;

namespace Pharmacy.Services.Modules.Products;


public interface IIncomingOrderService : IService<Guid>
{
    Task<BaseResponse> GetItems(Guid id);
    Task<BaseResponse> Create(IncomingOrderCreateDTO schema);
    Task<BaseResponse> Update(Guid id, IncomingOrderUpdateDTO schema);
}
