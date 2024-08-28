using Microsoft.AspNetCore.Mvc;
using Pharmacy.Shared.DTOs;
using Pharmacy.Shared.Responses;
using Pharmacy.Service.Interfaces;
using Pharmacy.Presentation.Utilities;

namespace Pharmacy.Presentation.Controllers;


[ApiController]
public class IncomingOrdersController : GenericController<Guid, IncomingOrderDTO>
{
    public IncomingOrdersController(IIncomingOrderService incomingOrderService) : base(incomingOrderService) {}

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
