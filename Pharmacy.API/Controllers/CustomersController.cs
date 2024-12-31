using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Interfaces;

namespace Pharmacy.Presentation.Controllers;

[Authorize]
public class CustomersController : ApiBaseController
{
    private readonly ICustomerService _service;
    public CustomersController(ICustomerService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        HandleResult(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) =>
        HandleResult(await _service.GetById(id));

    [HttpPost]
    public async Task<IActionResult> Create(CustomerCreateDTO customerDTO) =>
        HandleResult(await _service.Create(customerDTO));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, CustomerCreateDTO customerDTO) =>
        HandleResult(await _service.Update(id, customerDTO));

    [HttpGet("{customerId}/Payments")]
    public async Task<IActionResult> GetPayments(Guid customerId) =>
        HandleResult(await _service.GetPaymentOperations(customerId));

    [HttpPost("{customerId}/Payments")]
    public async Task<IActionResult> CreatePayments(Guid customerId, PaymentCreateDTO paymentDTO) =>
        HandleResult(await _service.AddPaymentOperation(customerId, paymentDTO));

    [HttpDelete("{customerId}/Payments/{paymentId}")]
    public async Task<IActionResult> DeletePayments(Guid customerId, int paymentId) =>
        HandleResult(await _service.RemovePaymentOperation(customerId, paymentId));
}
