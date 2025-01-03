using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Interfaces;

namespace Pharmacy.Presentation.Controllers;


[Authorize]
public class ScarceProductsController : ApiBaseController
{
    private readonly IScarceProductService _service;
    public ScarceProductsController(IScarceProductService productService) => _service = productService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScarceProductDTO>>> GetAll() =>
        HandleResult(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<ActionResult<ScarceProductDTO>> GetById(Guid id) =>
        HandleResult(await _service.GetById(id));

    [HttpPost]
    public async Task<ActionResult<ScarceProductDTO>> Create(ScarceProductCreateDTO product) =>
        HandleResult(await _service.Create(product));

    [HttpPut("{id}")]
    public async Task<ActionResult<ScarceProductDTO>> Update(Guid id, ScarceProductCreateDTO product) =>
        HandleResult(await _service.Update(id, product));

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id) =>
        HandleResult(await _service.Delete(id));
}
