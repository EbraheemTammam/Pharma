using Microsoft.AspNetCore.Mvc;
using Pharmacy.Presentation.Generics;
using Pharmacy.Application.Utilities;
using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Users.DTOs;
using Pharmacy.Services.Modules.Users;

namespace Pharmacy.Presentation.Controllers;


[ApiController]
public class UsersController : GenericController<int, UserDTO>
{
    public UsersController(IAuthService authService) : base(authService) {}

    [HttpPost]
    public async Task<IActionResult> Create(UserCreateDTO product) =>
        Ok(
            (await ((IAuthService)_service).Create(product)).GetResult<UserDTO>()
        );

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UserCreateDTO product)
    {
        BaseResponse response = await ((IAuthService)_service).Update(id, product);
        if(response.StatusCode != 200) return ProcessError(response);
        return Ok(response.GetResult<UserDTO>());
    }
}
