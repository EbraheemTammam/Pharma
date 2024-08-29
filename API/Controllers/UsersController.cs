using Microsoft.AspNetCore.Mvc;
using Pharmacy.Shared.DTOs;
using Pharmacy.Shared.Responses;
using Pharmacy.Service.Interfaces;
using Pharmacy.Presentation.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace Pharmacy.Presentation.Controllers;


[ApiController]
public class AuthController : GenericController<int, UserDTO>
{
    public AuthController(IAuthService authService) : base(authService) {}

    [HttpGet("Users"), Authorize(Roles = "Admin, Manager")]
    public async override Task<IActionResult> Get() => await base.Get();

    [HttpGet("Users/{id}"), Authorize(Roles = "Admin, Manager")]
    public async override Task<IActionResult> Get(int id) => await base.Get(id);

    [HttpPost("Register"), Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Create(UserCreateDTO user)
    {
        BaseResponse result = await ((IAuthService)_service).Create(user);
        if(result.StatusCode != 201) return ProcessError(result);
        var resultDTO = result.GetData<UserDTO>();
        return Created($"/api/Users/{resultDTO.Id}", resultDTO);
    }

    [HttpPut("Users/{id}"), Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Update(int id, UserCreateDTO product)
    {
        BaseResponse result = await ((IAuthService)_service).Update(id, product);
        if(result.StatusCode != 201) return ProcessError(result);
        return Created($"/api/Users/{id}", result.GetData<UserDTO>());
    }

    [HttpDelete("Users/{id}"), Authorize(Roles = "Admin, Manager")]
    public async override Task<IActionResult> Delete(int id) => await base.Delete(id);

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        BaseResponse result = await ((IAuthService)_service).Login(loginDTO);
        return result.StatusCode == 200 ? Ok() : ProcessError(result);
    }

    [HttpPost("Logout"), Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Logout()
    {
        BaseResponse result = await ((IAuthService)_service).Logout();
        return result.StatusCode == 200 ? Ok() : ProcessError(result);
    }
}
