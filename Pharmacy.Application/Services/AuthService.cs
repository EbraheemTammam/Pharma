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
    private User? _user;

    public AuthService(
        UserManager<User> userManger,
        IOptions<JwtSetting> options
    )
    {
        _userManger = userManger;
        _jwtSetting = options.Value;
    }

    public async Task<Result<TokenDTO>> RegisterAsync(RegisterDTO registerDTO)
    {
        var user = registerDTO.ToUserModel();
        var result = await _userManger.CreateAsync(user, registerDTO.Password);
        if (!result.Succeeded)
            return Result.Fail<TokenDTO>(AppResponses.BadRequestResponse("Invalid Register Try Again.."));
        _user = user;
        await _userManger.AddToRoleAsync(_user, Roles.User);
        return Result.Success(await CreateTokenAsync(withExpiryTime: true), StatusCodes.Status201Created);
    }

    public async Task<Result<TokenDTO>> LoginAsync(LoginDTO loginDTO)
    {
        _user = await _userManger.FindByEmailAsync(loginDTO.Email);
        return _user switch
        {
            null => Result.Fail<TokenDTO>(AppResponses.UnAuthorizedResponse),
            _ => Result.Success(await CreateTokenAsync(withExpiryTime: false))
        };
    }

    public async Task<Result<TokenDTO>> RefreshToken(TokenDTO tokenDTO)
    {
        var userId = GetUserIdFromExpiredToken(tokenDTO.AccessToken);
        if (userId is null) return Result.Fail<TokenDTO>(AppResponses.BadRequestResponse("Invalid Token"));
        _user = await _userManger.FindByIdAsync(userId);
        if (_user is null || _user.RefreshToken != tokenDTO.RefreshToken || _user.RefreshTokenExpiryTime <= DateTime.Now)
            return Result.Fail<TokenDTO>(AppResponses.BadRequestResponse("Invalid Token"));
        return Result.Success(await CreateTokenAsync(withExpiryTime: false));
    }

    private string? GetUserIdFromExpiredToken(string token)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(token);
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            // ValidAudience = _jwtSetting.ValidAudience,
            ValidIssuer = _jwtSetting.ValidIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecretKey))
        };
        var prinicpal = new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken is null || jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            return null;
        return prinicpal.FindFirstValue(JwtRegisteredClaimNames.UniqueName);
    }

    private async Task<TokenDTO> CreateTokenAsync(bool withExpiryTime)
    {
        var sigingCredentials = GetSigningCredentials();
        var claims = await GetClaimsAsync();
        var tokenOption = new JwtSecurityToken(
            issuer: _jwtSetting.ValidIssuer,
            audience: _jwtSetting.ValidAudience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSetting.ExpireOn),
            signingCredentials: sigingCredentials
        );
        _user!.RefreshToken = GenerateRefreshToken();
        if (withExpiryTime)
            _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(_jwtSetting.RefreshTokenExpireDays);
        await _userManger.UpdateAsync(_user);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOption);
        return new TokenDTO(accessToken, RefreshToken: _user!.RefreshToken);
    }

    private SigningCredentials GetSigningCredentials()
    {
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecretKey));
        return new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
    }

    private async Task<IEnumerable<Claim>> GetClaimsAsync()
    {
        ArgumentNullException.ThrowIfNull(_user);
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, _user.Email!),
            new Claim(JwtRegisteredClaimNames.UniqueName, _user.Id.ToString())
        };
        var roles = await _userManger.GetRolesAsync(_user);
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
