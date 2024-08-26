using Microsoft.AspNetCore.Mvc;
using Pharmacy.Presentation.Generics;
using Pharmacy.Application.Utilities;
using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Users.DTOs;
using Pharmacy.Services.Modules.Users;

namespace Pharmacy.Presentation.Modules.Users.Controllers;


[ApiController]
public class UsersController : GenericController<int, UserDTO>
{
    public UsersController(IAuthService authService) : base(authService) {}

    [HttpPost]
    public async Task<IActionResult> Create(UserCreateDTO product)
    {
        BaseResponse result = await ((IAuthService)_service).Create(product);
        return
        (
            result.StatusCode == 200 ? Ok(result.GetResult<UserDTO>())
            : ProcessError(result)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UserCreateDTO product)
    {
        BaseResponse result = await ((IAuthService)_service).Update(id, product);
        if(result.StatusCode != 200) return ProcessError(result);
        return
        (
            result.StatusCode == 200 ? Ok(result.GetResult<UserDTO>())
            : ProcessError(result)
        );
    }
}
