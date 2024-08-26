using Microsoft.AspNetCore.Mvc;
using Pharmacy.Shared.DTOs;
using Pharmacy.Shared.Responses;
using Pharmacy.Service.Interfaces;
using Pharmacy.Presentation.Utilities;

namespace Pharmacy.Presentation.Controllers;


[ApiController]
public class CustomersController : GenericController<Guid, CustomerDTO>
{
    public CustomersController(ICustomerService service) : base(service){}

    [HttpPost]
    public async Task<IActionResult> Create(CustomerCreateDTO customerDTO) =>
        Ok(
            (await ((ICustomerService)_service).Create(customerDTO))
            .GetResult<CustomerDTO>()
        );

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, CustomerCreateDTO customerDTO)
    {
        BaseResponse result = await ((ICustomerService)_service).Update(id, customerDTO);
        if(result.StatusCode != 200) return ProcessError(result);
        return Ok(result.GetResult<CustomerDTO>());
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
