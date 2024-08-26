using Pharmacy.Shared.Generics;
using Pharmacy.Shared.DTOs;

namespace Pharmacy.Service.Interfaces;


public interface IScarceProductService : IService<Guid>
{
    Task<BaseResponse> Create(ScarceProductCreateDTO schema);
    Task<BaseResponse> Update(Guid id, ScarceProductCreateDTO schema);
}
