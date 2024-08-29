using Microsoft.AspNetCore.Mvc;
using Pharmacy.Shared.Responses;
using Pharmacy.Service.Interfaces;
using Pharmacy.Presentation.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace Pharmacy.Presentation.Controllers;


[Route("Api/[controller]")]
public abstract class GenericController<TId, TResponseDTO>: ControllerBase
{
    protected readonly IService<TId> _service;

    public GenericController(IService<TId> service) =>
        _service = service;

    [HttpGet, Authorize]
    public async virtual Task<IActionResult> Get() =>
        Ok(
            (await _service.GetAll())
            .GetResult<IEnumerable<TResponseDTO>>()
        );

    [HttpGet("{id}"), Authorize]
    public async virtual Task<IActionResult> Get(TId id)
    {
        BaseResponse response = await _service.GetById(id);
        return response.StatusCode == 200 ? Ok(response.GetResult<TResponseDTO>()) : ProcessError(response);
    }

    [HttpDelete("{id}"), Authorize]
    public async virtual Task<IActionResult> Delete(TId id)
    {
        BaseResponse response = await _service.Delete(id);
        return response.StatusCode == 204 ? NoContent() : ProcessError(response);
    }

    protected IActionResult ProcessError(BaseResponse response)
    {
        return response.StatusCode switch
        {
            400 => BadRequest((BadRequestResponse)response),
            401 => Unauthorized((UnAuthorizedResponse)response),
            403 => StatusCode(response.StatusCode, ((ForbiddenResponse)response).Message),
            404 => NotFound((NotFoundResponse)response),
            500 => StatusCode(response.StatusCode, ((InternalServerErrorResponse)response).Message),
            _ => throw new NotImplementedException()
        };
    }
}
