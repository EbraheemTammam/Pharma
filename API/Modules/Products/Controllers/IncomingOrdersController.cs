using Microsoft.AspNetCore.Mvc;
using Pharmacy.Presentation.Generics;
using Pharmacy.Application.Utilities;
using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Products.DTOs;
using Pharmacy.Services.Modules.Products;

namespace Pharmacy.Presentation.Controllers;


[ApiController]
public class IncomingOrdersController : GenericController<Guid, IncomingOrderDTO>
{
    public IncomingOrdersController(IIncomingOrderService incomingOrderService) : base(incomingOrderService) {}
    [HttpGet]

    [HttpPost]
    public async Task<IActionResult> Create(IncomingOrderCreateDTO incomingOrder) =>
        Ok(
            (await ((IIncomingOrderService)_service).Create(incomingOrder)).GetResult<IncomingOrderDTO>()
        );

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, IncomingOrderUpdateDTO incomingOrder)
    {
        BaseResponse response = await ((IIncomingOrderService)_service).Update(id, incomingOrder);
        if(response.StatusCode != 200) ProcessError(response);
        return Ok(response.GetResult<IncomingOrderDTO>());
    }
}
