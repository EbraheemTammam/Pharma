using Pharmacy.Shared.Responses;
using Pharmacy.Shared.DTOs;

namespace Pharmacy.Application.Interfaces;


public interface IScarceProductService : IService<Guid>
{
    Task<BaseResponse> Create(ScarceProductCreateDTO schema);
    Task<BaseResponse> Update(Guid id, ScarceProductCreateDTO schema);
}
