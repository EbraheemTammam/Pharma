using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Modules.Users.Mappers;
using Pharmacy.Domain.Modules.Users.Models;
using Pharmacy.Services.Modules.Users;
using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Users.DTOs;

namespace Pharmacy.Application.Modules.Users.Services;


public class AuthService : IAuthService
{
    private readonly UserManager<CustomUser> _manager;
    private readonly PasswordHasher<CustomUser> _passwordHasher;

    public AuthService(UserManager<CustomUser> manager, PasswordHasher<CustomUser> hasher)
    {
        _manager = manager;
        _passwordHasher = hasher;
    }

    public async Task<BaseResponse> GetAll() =>
        new OkResponse<IEnumerable<UserDTO>>(
            (await _manager.Users.ToListAsync()).ConvertAll(obj => obj.ToDTO())
        );

    public async Task<BaseResponse> GetById(int id)
    {
        CustomUser? user = await _manager.FindByIdAsync(id.ToString());
        return (
            user is null ? new NotFoundResponse(id, nameof(CustomUser))
            : new OkResponse<UserDTO>(user.ToDTO())
        );
    }

    public async Task<BaseResponse> Add(UserCreateDTO user)
    {
        CustomUser newUser = user.ToModel();
        newUser.PasswordHash = _passwordHasher.HashPassword(newUser, user.Password);
        var result = await _manager.CreateAsync(newUser);
        if(result.Succeeded) return new OkResponse<UserDTO>(newUser.ToDTO());
        else return new InternalServerErrorResponse("User could not be created");
    }

    public async Task<BaseResponse> Update(int id, UserCreateDTO schema)
    {
        CustomUser? user = await _manager.FindByIdAsync(id.ToString());
        if(user is null) return new NotFoundResponse(id, nameof(CustomUser));
        user.PasswordHash = _passwordHasher.HashPassword(user, schema.Password);
        var result = await _manager.UpdateAsync(user);
        if(result.Succeeded) return new OkResponse<UserDTO>(user.ToDTO());
        else return new InternalServerErrorResponse("User could not be updated");
    }

    public async Task<BaseResponse> Delete(int id)
    {
        CustomUser? user = await _manager.FindByIdAsync(id.ToString());
        if(user is null) return new NotFoundResponse(id, nameof(CustomUser));
        var result = await _manager.DeleteAsync(user);
        if(result.Succeeded) return new NoContentResponse();
        else return new InternalServerErrorResponse("User could not be deleted");
    }
}
