using Pharmacy.Shared.Responses;
using Pharmacy.Shared.DTOs;

namespace Pharmacy.Service.Interfaces;


public interface IAuthService : IService<int>
{
    Task<BaseResponse> Create(UserCreateDTO user);
    Task<BaseResponse> Update(int id, UserCreateDTO user);
    Task<BaseResponse> Login(LoginDTO loginDTO);
    Task<BaseResponse> Logout();
}
