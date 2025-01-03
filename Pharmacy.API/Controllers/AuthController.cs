using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Presentation.Controllers;

public class AuthController : ApiBaseController
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
        => _authService = authService;

    [HttpPost("register")]
    public async Task<ActionResult<TokenDTO>> Register([FromBody] RegisterDTO registerDTO)
    {
        var result = await _authService.RegisterAsync(registerDTO);
        return HandleResult(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenDTO>> Login([FromBody] LoginDTO loginDTO)
    {
        var result = await _authService.LoginAsync(loginDTO);
        return HandleResult(result);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<TokenDTO>> Refresh([FromBody] TokenDTO tokenDTO)
    {
        var result = await _authService.RefreshToken(tokenDTO);
        return HandleResult(result);
    }
}
