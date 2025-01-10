using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Interfaces;
using Pharmacy.Application.DTOs;

namespace Pharmacy.Presentation.Controllers;

[Authorize]
public class OrdersController : ApiBaseController
{
    private readonly IOrderService _service;
    public OrdersController(IOrderService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAll([FromQuery] DateOnly? from, [FromQuery] DateOnly? to) =>
        HandleResult(await _service.GetAll(from, to));

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDTO>> GetById(Guid id) =>
        HandleResult(await _service.GetById(id));

    [HttpGet("{id}/Items")]
    public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetItems(Guid id) =>
        HandleResult(await _service.GetItems(id));

    [HttpPost]
    public async Task<ActionResult<OrderDTO>> Create(OrderCreateDTO orderDTO) =>
        HandleResult(await _service.Create(orderDTO));

    [HttpPut("{id}"), Authorize(Roles = "Admin, Manager")]
    public async Task<ActionResult<OrderDTO>> Update(Guid id, OrderUpdateDTO orderDTO) =>
        HandleResult(await _service.Update(id, orderDTO));

    [HttpDelete("{id}"), Authorize(Roles = "Admin, Manager")]
    public async Task<ActionResult> Delete(Guid id) =>
        HandleResult(await _service.Delete(id));
}
