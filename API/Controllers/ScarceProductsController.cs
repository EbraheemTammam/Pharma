using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Pharmacy.Application.DTOs;
using Pharmacy.Application.Responses;
using Pharmacy.Application.Interfaces;
using Pharmacy.Presentation.Utilities;

namespace Pharmacy.Presentation.Controllers;


[ApiController, Authorize]
public class ScarceProductsController : GenericController<Guid, ScarceProductDTO>
{
    public ScarceProductsController(IScarceProductService productService) : base(productService) {}

    [HttpPost]
    public async Task<IActionResult> Create(ScarceProductCreateDTO product)
    {
        BaseResponse response = await ((IScarceProductService)_service).Create(product);
        if(response.StatusCode != 201) return ProcessError(response);
        var result = response.GetData<ScarceProductDTO>();
        return Created($"/api/ScarceProducts/{result.Id}", result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ScarceProductCreateDTO product)
    {
        BaseResponse response = await ((IScarceProductService)_service).Update(id, product);
        if(response.StatusCode != 201) return ProcessError(response);
        return Created($"/api/ScarceProducts/{id}", response.GetData<ScarceProductDTO>());
    }
}
