using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Presentation.Utilities;
using Pharmacy.Service.Interfaces;
using Pharmacy.Shared.DTOs;
using Pharmacy.Shared.Responses;

namespace Pharmacy.Presentation.Controllers;



[ApiController, Authorize]
public class OrdersController : GenericController<Guid, OrderDTO>
{
    public OrdersController(IOrderService service) : base(service) {}

    [HttpPost]
    public async Task<IActionResult> Create(OrderCreateDTO orderDTO)
    {
        BaseResponse result = await ((IOrderService)_service).Create(orderDTO, User.Identity!.Name!);
        if(result.StatusCode != 201) return ProcessError(result);
        var response = result.GetData<OrderDTO>();
        return Created($"/Api/Orders/{response.Id}", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Guid id, OrderCreateDTO orderDTO)
    {
        BaseResponse result = await ((IOrderService)_service).Update(id, orderDTO, User.Identity!.Name!);
        if(result.StatusCode != 201) return ProcessError(result);
        return Created($"/Api/Orders/{id}", result.GetData<OrderDTO>());
    }
}
