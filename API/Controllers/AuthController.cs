using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Responses;
using Pharmacy.Application.Interfaces;
using Pharmacy.Presentation.Utilities;

namespace Pharmacy.Presentation.Controllers;


[ApiController]
public class AuthController : GenericController<int, UserDTO>
{
    public AuthController(IAuthService authService) : base(authService) {}

    [HttpPost("Register"), Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Create(UserCreateDTO user)
    {
        BaseResponse result = await ((IAuthService)_service).Create(user);
        if(result.StatusCode != 201) return ProcessError(result);
        var resultDTO = result.GetData<UserDTO>();
        return Created($"/api/Users/{resultDTO.Id}", resultDTO);
    }

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

    [HttpGet("AccessDenied")]
    public IActionResult AccessDenied() =>
        Forbid("Permission Denied");
}
