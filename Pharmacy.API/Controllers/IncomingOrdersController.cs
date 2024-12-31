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
    public async Task<IActionResult> GetAll() =>
        HandleResult(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) =>
        HandleResult(await _service.GetById(id));

    [HttpGet("{id}/Items")]
    public async Task<IActionResult> GetItems(Guid id) =>
        HandleResult(await _service.GetItems(id));

    [HttpPost]
    public async Task<IActionResult> Create(IncomingOrderCreateDTO incomingOrder) =>
        HandleResult(await _service.Create(incomingOrder));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, IncomingOrderUpdateDTO incomingOrder) =>
        HandleResult(await _service.Update(id, incomingOrder));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) =>
        HandleResult(await _service.Delete(id));
}
