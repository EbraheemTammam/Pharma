using Microsoft.AspNetCore.Mvc;
using Pharmacy.Presentation.Generics;
using Pharmacy.Application.Utilities;
using Pharmacy.Services;
using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Products.DTOs;

namespace Pharmacy.Presentation.Controllers;


[ApiController]
public class IncomingOrdersController : GenericController
{
    private readonly IIncomingOrderService _incomingOrderService;
    public IncomingOrdersController(IIncomingOrderService incomingOrderService) =>
        _incomingOrderService = incomingOrderService;

    [HttpGet]
    public IActionResult Get() =>
        Ok(
            _incomingOrderService.GetAll()
            .GetResult<IEnumerable<IncomingOrderDTO>>()
        );

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        BaseResponse response = _incomingOrderService.GetById(id);
        if(!response.Success) ProcessError(response);
        return Ok(response.GetResult<IncomingOrderDTO>());
    }

    [HttpPost]
    public IActionResult Create(IncomingOrderCreateDTO incomingOrder) =>
        Ok(
            _incomingOrderService.Create(incomingOrder).GetResult<IncomingOrderDTO>()
        );

    [HttpPut]
    public IActionResult Update(Guid id, IncomingOrderUpdateDTO incomingOrder)
    {
        BaseResponse response = _incomingOrderService.Update(id, incomingOrder);
        if(!response.Success) ProcessError(response);
        return Ok(response.GetResult<IncomingOrderDTO>());
    }

    [HttpDelete]
    public IActionResult Delete(Guid id)
    {
        BaseResponse response = _incomingOrderService.Delete(id);
        if(!response.Success) ProcessError(response);
        return NoContent();
    }
}
