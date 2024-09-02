using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Interfaces;


public interface IAuthService : IService<int>
{
    Task<BaseResponse> Create(UserCreateDTO user);
    Task<BaseResponse> Update(int id, UserCreateDTO user);
    Task<BaseResponse> Login(LoginDTO loginDTO);
    Task<BaseResponse> Logout();
}
