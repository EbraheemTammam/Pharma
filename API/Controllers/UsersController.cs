using Microsoft.AspNetCore.Mvc;
using Pharmacy.Shared.DTOs;
using Pharmacy.Shared.Responses;
using Pharmacy.Service.Interfaces;
using Pharmacy.Presentation.Utilities;

namespace Pharmacy.Presentation.Controllers;


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
