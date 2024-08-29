using Microsoft.AspNetCore.Mvc;
using Pharmacy.Shared.DTOs;
using Pharmacy.Shared.Responses;
using Pharmacy.Service.Interfaces;
using Pharmacy.Presentation.Utilities;

namespace Pharmacy.Presentation.Controllers;


[ApiController]
public class ProductProvidersController : GenericController<Guid, ProductProviderDTO>
{
    public ProductProvidersController(IProductProviderService productProviderService) : base(productProviderService) {}

    [HttpPost]
    public async Task<IActionResult> Create(ProductProviderCreateDTO productProvider)
    {
        BaseResponse response = await ((IProductProviderService)_service).Create(productProvider);
        if(response.StatusCode != 201) return ProcessError(response);
        var result = response.GetData<ProductProviderDTO>();
        return Created($"/api/ProductProviders/{result.Id}", result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ProductProviderCreateDTO productProvider)
    {
        BaseResponse response = await ((IProductProviderService)_service).Update(id, productProvider);
        if(response.StatusCode != 201) return ProcessError(response);
        return Created($"/api/ProductProviders/{id}", response.GetData<ProductProviderDTO>());
    }
}
