using Microsoft.AspNetCore.Mvc;
using Pharmacy.Shared.DTOs;
using Pharmacy.Shared.Responses;
using Pharmacy.Service.Interfaces;
using Pharmacy.Application.Utilities;

namespace Pharmacy.Presentation.Controllers;


[ApiController]
public class ProductsController : GenericController<Guid, ProductDTO>
{
    public ProductsController(IProductService productService) : base(productService) {}

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDTO product) =>
        Ok(
            (await ((IProductService)_service).Create(product)).GetResult<ProductDTO>()
        );

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ProductCreateDTO product)
    {
        BaseResponse response = await ((IProductService)_service).Update(id, product);
        if(response.StatusCode != 200) return ProcessError(response);
        return Ok(response.GetResult<ProductDTO>());
    }
}
