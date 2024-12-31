using Pharmacy.Application.Responses;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Interfaces;

public interface IUserService
{
    Task<Result<IEnumerable<UserDTO>>> GetAllAsync();
    Task<Result<UserDTO>> GetByIdAsync(int id);
    Task<Result<UserDTO>> UpdateAsync(int id, RegisterDTO updateDTO);
    Task<Result> DeleteAsync(int id);
}
