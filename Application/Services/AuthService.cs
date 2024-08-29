using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Mappers;
using Pharmacy.Domain.Models;
using Pharmacy.Service.Interfaces;
using Pharmacy.Shared.Responses;
using Pharmacy.Shared.DTOs;

namespace Pharmacy.Application.Services;


public class AuthService : IAuthService
{
    private readonly UserManager<User> _manager;
    private readonly SignInManager<User> _signInManager;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthService(UserManager<User> manager, PasswordHasher<User> hasher, SignInManager<User> signInManager)
    {
        _manager = manager;
        _passwordHasher = hasher;
        _signInManager = signInManager;
    }

    public async Task<BaseResponse> GetAll() =>
        new OkResponse<IEnumerable<UserDTO>>(
            (await _manager.Users.ToListAsync()).ConvertAll(obj => obj.ToDTO())
        );

    public async Task<BaseResponse> GetById(int id)
    {
        User? user = await _manager.FindByIdAsync(id.ToString());
        return (
            user is null ? new NotFoundResponse(id, nameof(User))
            : new OkResponse<UserDTO>(user.ToDTO())
        );
    }

    public async Task<BaseResponse> Create(UserCreateDTO user)
    {
        User newUser = user.ToModel();
        newUser.PasswordHash = _passwordHasher.HashPassword(newUser, user.Password);
        var result = await _manager.CreateAsync(newUser);
        if(!result.Succeeded)
            return new InternalServerErrorResponse("User could not be created");
        await _manager.AddToRoleAsync(newUser, user.IsManager ? "Manager" : "Employee");
        return new CreatedResponse<UserDTO>(newUser.ToDTO());
    }

    public async Task<BaseResponse> Update(int id, UserCreateDTO schema)
    {
        User? user = await _manager.FindByIdAsync(id.ToString());
        if(user is null) return new NotFoundResponse(id, nameof(User));
        user.Update(schema);
        user.PasswordHash = _passwordHasher.HashPassword(user, schema.Password);
        var result = await _manager.UpdateAsync(user);
        if(result.Succeeded) return new CreatedResponse<UserDTO>(user.ToDTO());
        else return new InternalServerErrorResponse("User could not be updated");
    }

    public async Task<BaseResponse> Delete(int id)
    {
        User? user = await _manager.FindByIdAsync(id.ToString());
        if(user is null) return new NotFoundResponse(id, nameof(User));
        var result = await _manager.DeleteAsync(user);
        if(result.Succeeded) return new NoContentResponse();
        else return new InternalServerErrorResponse("User could not be deleted");
    }

    public async Task<BaseResponse> Login(LoginDTO loginDTO)
    {
        User? user = await _manager.FindByNameAsync(loginDTO.Email);
        if(user is not null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);
            if(result.Succeeded) return new OkResponse<bool>(true);
        }
        return new UnAuthorizedResponse();
    }

    public async Task<BaseResponse> Logout()
    {
        await _signInManager.SignOutAsync();
        return new OkResponse<bool>(true);
    }
}
