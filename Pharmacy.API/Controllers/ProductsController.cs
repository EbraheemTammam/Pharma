using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Responses;
using Pharmacy.Application.Interfaces;
using Pharmacy.Presentation.Utilities;

namespace Pharmacy.Presentation.Controllers;


[ApiController, Authorize]
public class ProductsController : GenericController<Guid, ProductDTO>
{
    public ProductsController(IProductService productService) : base(productService) {}

    [HttpGet("Lacked")]
    public async Task<IActionResult> GetLacked() =>
        Ok((await ((IProductService)_service).GetLacked()).GetResult<IEnumerable<ProductDTO>>());

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDTO product)
    {
        BaseResponse response = await ((IProductService)_service).Create(product);
        if(response.StatusCode != 201) return ProcessError(response);
        var result = response.GetData<ProductDTO>();
        return Created($"/api/Products/{result.Id}", result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ProductCreateDTO product)
    {
        BaseResponse response = await ((IProductService)_service).Update(id, product);
        if(response.StatusCode != 201) return ProcessError(response);
        return Created($"/api/Products/{id}", response.GetData<ProductDTO>());
    }
}
