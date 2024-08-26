using Microsoft.AspNetCore.Mvc;
using Pharmacy.Shared.Responses;
using Pharmacy.Service.Interfaces;
using Pharmacy.Application.Utilities;

namespace Pharmacy.Presentation.Controllers;


[Route("api/[controller]")]
public abstract class GenericController<TId, TResponseDTO>: ControllerBase
{
    protected readonly IService<TId> _service;

    public GenericController(IService<TId> service) =>
        _service = service;

    [HttpGet]
    public async virtual Task<IActionResult> Get() =>
        Ok(
            (await _service.GetAll())
            .GetResult<IEnumerable<TResponseDTO>>()
        );

    [HttpGet("{id}")]
    public async virtual Task<IActionResult> Get(TId id)
    {
        BaseResponse response = await _service.GetById(id);
        if(response.StatusCode != 200) return ProcessError(response);
        return Ok(response.GetResult<TResponseDTO>());
    }

    [HttpDelete("{id}")]
    public async virtual Task<IActionResult> Delete(TId id)
    {
        BaseResponse response = await _service.Delete(id);
        if(response.StatusCode != 204) return ProcessError(response);
        return NoContent();
    }

    protected IActionResult ProcessError(BaseResponse response)
    {
        return response.StatusCode switch
        {
            404 => NotFound((NotFoundResponse)response),
            400 => BadRequest((BadRequestResponse)response),
            500 => StatusCode(response.StatusCode, ((InternalServerErrorResponse)response).Message),
            _ => throw new NotImplementedException()
        };
    }
}
