using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Interfaces;


public interface IScarceProductService : IService<Guid>
{
    Task<BaseResponse> Create(ScarceProductCreateDTO schema);
    Task<BaseResponse> Update(Guid id, ScarceProductCreateDTO schema);
}
