using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Interfaces;


public interface IProductService : IService<Guid>
{
    Task<BaseResponse> GetLacked();
    Task<BaseResponse> Create(ProductCreateDTO schema);
    Task<BaseResponse> Update(Guid id, ProductCreateDTO schema);
    Task<BaseResponse> GetAboutToExpire();
}
