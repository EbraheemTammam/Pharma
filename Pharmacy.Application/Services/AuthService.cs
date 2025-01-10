using Pharmacy.Application.Responses;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.Mappers;
using Pharmacy.Application.Utilities;
using Pharmacy.Domain.Models;
using Pharmacy.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Pharmacy.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManger;
    private readonly JwtSetting _jwtSetting;

    public AuthService(UserManager<User> userManger, IOptions<JwtSetting> options)
    {
        _userManger = userManger;
        _jwtSetting = options.Value;
    }

    public async Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
    {
        var user = registerDTO.ToUserModel();
        var result = await _userManger.CreateAsync(user, registerDTO.Password);
        if (!result.Succeeded)
            return Result.Fail<UserDTO>(AppResponses.BadRequestResponse(result.Errors.First().Description));
        if(registerDTO.IsManager)
            await _userManger.AddToRoleAsync(user, Roles.Manager);
        else await _userManger.AddToRoleAsync(user, Roles.Employee);
        return Result.Success(user.ToUserDTO(), StatusCodes.Status201Created);
    }

    public async Task<Result<TokenDTO>> LoginAsync(LoginDTO loginDTO)
    {
        User? user = await _userManger.FindByNameAsync(loginDTO.Username);
        return user switch
        {
            null => Result.Fail<TokenDTO>(AppResponses.UnAuthorizedResponse),
            _ => Result.Success(await CreateTokenAsync(user, withExpiryTime: false))
        };
    }

    public async Task<Result<TokenDTO>> RefreshToken(TokenDTO tokenDTO)
    {
        var userId = GetUserIdFromExpiredToken(tokenDTO.AccessToken);
        if (userId is null) return Result.Fail<TokenDTO>(AppResponses.BadRequestResponse("Invalid Token"));
        User? user = await _userManger.FindByIdAsync(userId);
        if (user is null || user.RefreshToken != tokenDTO.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            return Result.Fail<TokenDTO>(AppResponses.UnAuthorizedResponse);
        return Result.Success(await CreateTokenAsync(user, withExpiryTime: false));
    }

    private string? GetUserIdFromExpiredToken(string token)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(token);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtSetting.ValidIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecretKey)),
            ValidateLifetime = false // Disable lifetime validation to allow expired tokens
        };

        var principal = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            return null;
        }
        return principal.FindFirstValue(ClaimTypes.Sid);
    }

    private async Task<TokenDTO> CreateTokenAsync(User user, bool withExpiryTime)
    {
        var sigingCredentials = GetSigningCredentials();
        var claims = await GetClaimsAsync(user);
        var tokenOption = new JwtSecurityToken(
            issuer: _jwtSetting.ValidIssuer,
            audience: _jwtSetting.ValidAudience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSetting.ExpireOn),
            signingCredentials: sigingCredentials
        );
        user!.RefreshToken = GenerateRefreshToken();
        if (withExpiryTime)
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtSetting.RefreshTokenExpireDays);
        await _userManger.UpdateAsync(user);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOption);
        return new TokenDTO(accessToken, RefreshToken: user!.RefreshToken);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecretKey));
        return new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
    }

    private async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Sid, user.Id.ToString())
        };
        var roles = await _userManger.GetRolesAsync(user);
        //claims.Add(new Claim("Roles", JsonSerializer.Serialize(roles)));
        claims.AddRange(roles?.Select(role => new Claim(ClaimTypes.Role, role)) ?? []);
        return claims;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
