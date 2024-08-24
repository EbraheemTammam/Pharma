using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Products.DTOs;

namespace Pharmacy.Services.Interfaces;


public interface IService<TId>
{
    Task<BaseResponse> GetAll();
    Task<BaseResponse> GetById(TId id);
    Task<BaseResponse> Delete(TId id);
}
