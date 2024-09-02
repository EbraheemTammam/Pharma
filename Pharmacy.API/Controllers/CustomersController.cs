using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Responses;
using Pharmacy.Application.Interfaces;
using Pharmacy.Presentation.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace Pharmacy.Presentation.Controllers;


[ApiController, Authorize]
public class CustomersController : GenericController<Guid, CustomerDTO>
{
    public CustomersController(ICustomerService service) : base(service){}

    [HttpPost]
    public async Task<IActionResult> Create(CustomerCreateDTO customerDTO)
    {
        BaseResponse response = await ((ICustomerService)_service).Create(customerDTO);
        if(response.StatusCode != 201) return ProcessError(response);
        var result = response.GetData<CustomerDTO>();
        return Created($"/api/Customers/{result.Id}", result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, CustomerCreateDTO customerDTO)
    {
        BaseResponse result = await ((ICustomerService)_service).Update(id, customerDTO);
        if(result.StatusCode != 201) return ProcessError(result);
        return Created($"/api/Customers/{id}", result.GetData<CustomerDTO>());
    }

    [HttpGet("{customerId}/Payments")]
    public async Task<IActionResult> GetPayments(Guid customerId) =>
        Ok(
            (await ((ICustomerService)_service).GetPaymentOperations(customerId))
            .GetResult<IEnumerable<PaymentDTO>>()
        );

    [HttpPost("{customerId}/Payments")]
    public async Task<IActionResult> CreatePayments(Guid customerId, PaymentCreateDTO paymentDTO)
    {
        BaseResponse result = await ((ICustomerService)_service).AddPaymentOperation(customerId, paymentDTO);
        if(result.StatusCode != 200) return ProcessError(result);
        return Ok(result.GetResult<PaymentDTO>());
    }

    [HttpDelete("{customerId}/Payments/{paymentId}")]
    public async Task<IActionResult> DeletePayments(Guid customerId, int paymentId)
    {
        BaseResponse result = await ((ICustomerService)_service).RemovePaymentOperation(customerId, paymentId);
        if(result.StatusCode != 204) return ProcessError(result);
        return NoContent();
    }
}
