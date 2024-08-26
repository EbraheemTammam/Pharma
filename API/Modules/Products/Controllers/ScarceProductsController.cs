using Microsoft.AspNetCore.Mvc;
using Pharmacy.Presentation.Generics;
using Pharmacy.Application.Utilities;
using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Products.DTOs;
using Pharmacy.Services.Modules.Products;

namespace Pharmacy.Presentation.Modules.Products.Controllers;


[ApiController]
public class ScarceProductsController : GenericController<Guid, ScarceProductDTO>
{
    public ScarceProductsController(IScarceProductService productService) : base(productService) {}

    [HttpPost]
    public async Task<IActionResult> Create(ScarceProductCreateDTO product) =>
        Ok(
            (await ((IScarceProductService)_service).Create(product)).GetResult<ScarceProductDTO>()
        );

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ScarceProductCreateDTO product)
    {
        BaseResponse response = await ((IScarceProductService)_service).Update(id, product);
        if(response.StatusCode != 200) return ProcessError(response);
        return Ok(response.GetResult<ScarceProductDTO>());
    }
}
