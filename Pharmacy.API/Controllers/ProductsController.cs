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
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll() =>
        HandleResult(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetById(Guid id) =>
        HandleResult(await _service.GetById(id));

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> Create(ProductCreateDTO product) =>
        HandleResult(await _service.Create(product));

    [HttpPatch("{id}")]
    public async Task<ActionResult<ProductDTO>> Update(Guid id, ProductCreateDTO product) =>
        HandleResult(await _service.Update(id, product));

    // [HttpDelete("{id}")]
    // public async Task<ActionResult> Delete(Guid id) =>
    //     HandleResult(await _service.Delete(id));

    [HttpGet("AboutToExpire")]
    public async Task<ActionResult<IEnumerable<ProductItemDTO>>> GetAboutToExpire() =>
        HandleResult(await _service.GetAboutToExpire());
}
