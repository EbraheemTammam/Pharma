using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Pharmacy.Presentation.Controllers;

[Authorize]
public class IncomingOrdersController : ApiBaseController
{
    private readonly IIncomingOrderService _service;
    public IncomingOrdersController(IIncomingOrderService incomingOrderService) => _service = incomingOrderService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IncomingOrderDTO>>> GetAll() =>
        HandleResult(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<ActionResult<IncomingOrderDTO>> GetById(Guid id) =>
        HandleResult(await _service.GetById(id));

    [HttpGet("{id}/Items")]
    public async Task<ActionResult<IEnumerable<ProductItemDTO>>> GetItems(Guid id) =>
        HandleResult(await _service.GetItems(id));

    [HttpPost]
    public async Task<ActionResult<IncomingOrderDTO>> Create(IncomingOrderCreateDTO incomingOrder) =>
        HandleResult(await _service.Create(incomingOrder));

    [HttpPut("{id}")]
    public async Task<ActionResult<IncomingOrderDTO>> Update(Guid id, IncomingOrderUpdateDTO incomingOrder) =>
        HandleResult(await _service.Update(id, incomingOrder));

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id) =>
        HandleResult(await _service.Delete(id));
}
