using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Interfaces;

namespace Pharmacy.Presentation.Controllers;

[Authorize]
public class ProductsController : ApiBaseController
{
    private readonly IProductService _service;
    public ProductsController(IProductService productService) => _service = productService;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        HandleResult(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) =>
        HandleResult(await _service.GetById(id));

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDTO product) =>
        HandleResult(await _service.Create(product));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ProductCreateDTO product) =>
        HandleResult(await _service.Update(id, product));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) =>
        HandleResult(await _service.Delete(id));

    [HttpGet("AboutToExpire")]
    public async Task<IActionResult> GetAboutToExpire() =>
        HandleResult(await _service.GetAboutToExpire());
}
