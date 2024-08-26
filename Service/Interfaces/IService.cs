using Pharmacy.Shared.Responses;

namespace Pharmacy.Service.Interfaces;


public interface IService<TId>
{
    Task<BaseResponse> GetAll();
    Task<BaseResponse> GetById(TId id);
    Task<BaseResponse> Delete(TId id);
}
