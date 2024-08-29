using Microsoft.AspNetCore.Mvc;
using Pharmacy.Shared.DTOs;
using Pharmacy.Shared.Responses;
using Pharmacy.Service.Interfaces;
using Pharmacy.Presentation.Utilities;

namespace Pharmacy.Presentation.Controllers;


[ApiController]
public class AuthController : GenericController<int, UserDTO>
{
    public AuthController(IAuthService authService) : base(authService) {}

    [HttpGet("Users")]
    public async override Task<IActionResult> Get() => await base.Get();

    [HttpGet("Users/{id}")]
    public async override Task<IActionResult> Get(int id) => await base.Get(id);

    [HttpPost("Users")]
    public async Task<IActionResult> Create(UserCreateDTO user)
    {
        BaseResponse result = await ((IAuthService)_service).Create(user);
        if(result.StatusCode != 201) return ProcessError(result);
        var resultDTO = result.GetData<UserDTO>();
        return Created($"/api/Users/{resultDTO.Id}", resultDTO);
    }

    [HttpPut("Users/{id}")]
    public async Task<IActionResult> Update(int id, UserCreateDTO product)
    {
        BaseResponse result = await ((IAuthService)_service).Update(id, product);
        if(result.StatusCode != 201) return ProcessError(result);
        return Created($"/api/Users/{id}", result.GetData<UserDTO>());
    }

    [HttpDelete("Users/{id}")]
    public async override Task<IActionResult> Delete(int id) => await base.Delete(id);

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        BaseResponse result = await ((IAuthService)_service).Login(loginDTO);
        return result.StatusCode == 200 ? Ok() : ProcessError(result);
    }

    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        BaseResponse result = await ((IAuthService)_service).Logout();
        return result.StatusCode == 200 ? Ok() : ProcessError(result);
    }
}
