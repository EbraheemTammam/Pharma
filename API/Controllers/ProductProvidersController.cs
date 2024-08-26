using Microsoft.AspNetCore.Mvc;
using Pharmacy.Shared.DTOs;
using Pharmacy.Shared.Responses;
using Pharmacy.Service.Interfaces;
using Pharmacy.Application.Utilities;

namespace Pharmacy.Presentation.Controllers;


[ApiController]
public class ProductProvidersController : GenericController<Guid, ProductProviderDTO>
{
    public ProductProvidersController(IProductProviderService productProviderService) : base(productProviderService) {}

    [HttpPost]
    public async Task<IActionResult> Create(ProductProviderCreateDTO productProvider) =>
        Ok(
            (await ((IProductProviderService)_service).Create(productProvider)).GetResult<ProductProviderDTO>()
        );

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ProductProviderCreateDTO productProvider)
    {
        BaseResponse response = await ((IProductProviderService)_service).Update(id, productProvider);
        if(response.StatusCode != 200) ProcessError(response);
        return Ok(response.GetResult<ProductProviderDTO>());
    }
}
