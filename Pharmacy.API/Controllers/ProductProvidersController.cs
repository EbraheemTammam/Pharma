using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Interfaces;

namespace Pharmacy.Presentation.Controllers;

[Authorize]
public class ProductProvidersController : ApiBaseController
{
    private readonly IProductProviderService _service;
    public ProductProvidersController(IProductProviderService productProviderService) => _service = productProviderService;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        HandleResult(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) =>
        HandleResult(await _service.GetById(id));

    [HttpPost]
    public async Task<IActionResult> Create(ProductProviderCreateDTO productProvider) =>
        HandleResult(await _service.Create(productProvider));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ProductProviderCreateDTO productProvider) =>
        HandleResult(await _service.Update(id, productProvider));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) =>
        HandleResult(await _service.Delete(id));
}
