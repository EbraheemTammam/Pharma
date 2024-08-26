using Pharmacy.Services.Interfaces;
using Pharmacy.Shared.Generics;
using Pharmacy.Shared.DTOs;

namespace Pharmacy.Service.Services;


public interface IAuthService : IService<int>
{
    Task<BaseResponse> Create(UserCreateDTO user);
    Task<BaseResponse> Update(int id, UserCreateDTO user);
}
