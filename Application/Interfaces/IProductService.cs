using Pharmacy.Shared.Responses;
using Pharmacy.Shared.DTOs;

namespace Pharmacy.Application.Interfaces;


public interface IProductService : IService<Guid>
{
    Task<BaseResponse> GetLacked();
    Task<BaseResponse> Create(ProductCreateDTO schema);
    Task<BaseResponse> Update(Guid id, ProductCreateDTO schema);
}
