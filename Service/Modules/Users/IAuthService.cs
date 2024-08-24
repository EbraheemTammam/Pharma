using Pharmacy.Services.Interfaces;
using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Users.DTOs;

namespace Pharmacy.Services.Modules.Users;


public interface IAuthService : IService<int>
{
    Task<BaseResponse> Add(UserCreateDTO user);
    Task<BaseResponse> Update(int id, UserCreateDTO user);
}
