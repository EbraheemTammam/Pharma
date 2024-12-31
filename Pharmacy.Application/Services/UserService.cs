using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.Mappers;
using Pharmacy.Application.Responses;
using Pharmacy.Domain.Models;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Application.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _manager;

    public UserService(UserManager<User> manager) =>
        _manager = manager;

    public async Task<Result<IEnumerable<UserDTO>>> GetAllAsync() =>
        Result.Success(
            (IEnumerable<UserDTO>)
            (await _manager.Users.ToListAsync())
            .ConvertAll(user => user.ToUserDTO())
        );

    public async Task<Result<UserDTO>> GetByIdAsync(int id)
    {
        User? user = await _manager.FindByIdAsync(id.ToString());
        return user switch
        {
            null => Result.Fail<UserDTO>(AppResponses.NotFoundResponse(id, nameof(User))),
            _ => Result.Success(user.ToUserDTO())
        };
    }

    public async Task<Result<UserDTO>> UpdateAsync(int id, RegisterDTO updateDTO)
    {
        User? user = await _manager.FindByIdAsync(id.ToString());
        if (user is null) return Result.Fail<UserDTO>(AppResponses.NotFoundResponse(id, nameof(User)));
        user.Update(updateDTO);
        await _manager.UpdateAsync(user);
        return Result.Success(user.ToUserDTO(), StatusCodes.Status201Created);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        User? user = await _manager.FindByIdAsync(id.ToString());
        if (user is null) return Result.Fail(AppResponses.NotFoundResponse(id, nameof(User)));
        await _manager.DeleteAsync(user);
        return Result.Success(StatusCodes.Status204NoContent);
    }
}
