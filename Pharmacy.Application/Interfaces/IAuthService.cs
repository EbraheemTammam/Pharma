using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Interfaces;

public interface IAuthService
{
    Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO);
    Task<Result<TokenDTO>> LoginAsync(LoginDTO loginDTO);
    Task<Result<TokenDTO>> RefreshToken(TokenDTO tokenDTO);
}
