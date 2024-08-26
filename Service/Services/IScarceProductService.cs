using Pharmacy.Services.Interfaces;
using Pharmacy.Shared.Generics;
using Pharmacy.Shared.DTOs;

namespace Pharmacy.Service.Services;


public interface IScarceProductService : IService<Guid>
{
    Task<BaseResponse> Create(ScarceProductCreateDTO schema);
    Task<BaseResponse> Update(Guid id, ScarceProductCreateDTO schema);
}
