using Microsoft.AspNetCore.Mvc;
using Pharmacy.Shared.Generics;

namespace Pharmacy.Presentation.Generics;


[Route("api/[controller]")]
public class GenericController: ControllerBase
{
    protected IActionResult ProcessError(BaseResponse response)
    {
        return response.StatusCode switch
        {
            404 => NotFound(response),
            400 => BadRequest(response),
            _ => throw new NotImplementedException()
        };
    }
}
