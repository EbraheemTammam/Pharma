using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Responses;
using Pharmacy.Application.Interfaces;
using Pharmacy.Presentation.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace Pharmacy.Presentation.Controllers;


[ApiController, Authorize]
public class IncomingOrdersController : GenericController<Guid, IncomingOrderDTO>
{
    public IncomingOrdersController(IIncomingOrderService incomingOrderService) : base(incomingOrderService) {}

    [HttpPost]
    public async Task<IActionResult> Create(IncomingOrderCreateDTO incomingOrder)
    {
        BaseResponse response = await ((IIncomingOrderService)_service).Create(incomingOrder);
        if(response.StatusCode != 201) return ProcessError(response);
        var result = response.GetData<IncomingOrderDTO>();
        return Created($"/api/IncomingOrders/{result.Id}", result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, IncomingOrderUpdateDTO incomingOrder)
    {
        BaseResponse response = await ((IIncomingOrderService)_service).Update(id, incomingOrder);
        if(response.StatusCode != 201) return ProcessError(response);
        return Created($"/api/IncomingOrders/{id}", response.GetData<IncomingOrderDTO>());
    }
}
