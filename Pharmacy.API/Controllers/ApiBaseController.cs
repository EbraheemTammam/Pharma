using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Responses;

namespace Pharmacy.Presentation.Controllers;

[ApiController]
[Route("Api/[controller]")]
public abstract class ApiBaseController : ControllerBase
{
    protected IActionResult HandleResult<TResult>(Result<TResult> result)
    {
        if(!result.Succeeded) return ProcessError(result.Response);
        return result.Response.StatusCode switch
        {
            StatusCodes.Status200OK => Ok(result.Data),
            StatusCodes.Status201Created => StatusCode(StatusCodes.Status201Created, result.Data),
            _ => throw new NotImplementedException()
        };
    }

    protected IActionResult HandleResult(Result result)
    {
        if (!result.Succeeded) return ProcessError(result.Response);
        return result.Response.StatusCode switch
        {
            StatusCodes.Status204NoContent => NoContent(),
            _ => throw new NotImplementedException()
        };
    }

    protected IActionResult ProcessError(Response response)
    {
        return response.StatusCode switch
        {
            StatusCodes.Status400BadRequest => BadRequest(response),
            StatusCodes.Status401Unauthorized => Unauthorized(response),
            StatusCodes.Status403Forbidden => Forbid(response.Message),
            StatusCodes.Status404NotFound => NotFound(response),
            StatusCodes.Status500InternalServerError => StatusCode(response.StatusCode, response.Message),
            _ => throw new NotImplementedException()
        };
    }
}
