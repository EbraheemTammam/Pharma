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
    public async Task<ActionResult<UserDTO>> Register([FromBody] RegisterDTO registerDTO) =>
        HandleResult(await _authService.RegisterAsync(registerDTO));

    [HttpPost("login")]
    public async Task<ActionResult<TokenDTO>> Login([FromBody] LoginDTO loginDTO) =>
        HandleResult(await _authService.LoginAsync(loginDTO));

    [HttpPost("refresh")]
    public async Task<ActionResult<TokenDTO>> Refresh([FromBody] TokenDTO tokenDTO) =>
        HandleResult(await _authService.RefreshToken(tokenDTO));
}
