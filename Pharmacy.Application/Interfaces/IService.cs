using Pharmacy.Application.Responses;

namespace Pharmacy.Application.Interfaces;


public interface IService<TId>
{
    Task<BaseResponse> GetAll();
    Task<BaseResponse> GetById(TId id);
    Task<BaseResponse> Delete(TId id);
}
