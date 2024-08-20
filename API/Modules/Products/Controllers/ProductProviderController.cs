using Microsoft.AspNetCore.Mvc;
using Pharmacy.Presentation.Generics;
using Pharmacy.Application.Utilities;
using Pharmacy.Services;
using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Products.DTOs;

namespace Pharmacy.Presentation.Controllers;


[ApiController]
public class ProductProvidersController : GenericController
{
    private readonly IProductProviderService _productProviderService;
    public ProductProvidersController(IProductProviderService productProviderService) =>
        _productProviderService = productProviderService;

    [HttpGet]
    public IActionResult Get() =>
        Ok(
            _productProviderService.GetAll()
            .GetResult<IEnumerable<ProductProviderDTO>>()
        );

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        BaseResponse response = _productProviderService.GetById(id);
        if(!response.Success) ProcessError(response);
        return Ok(response.GetResult<ProductProviderDTO>());
    }

    [HttpPost]
    public IActionResult Create(ProductProviderCreateDTO productProvider) =>
        Ok(
            _productProviderService.Create(productProvider).GetResult<ProductProviderDTO>()
        );

    [HttpPut]
    public IActionResult Update(Guid id, ProductProviderCreateDTO productProvider)
    {
        BaseResponse response = _productProviderService.Update(id, productProvider);
        if(!response.Success) ProcessError(response);
        return Ok(response.GetResult<ProductProviderDTO>());
    }

    [HttpDelete]
    public IActionResult Delete(Guid id)
    {
        BaseResponse response = _productProviderService.Delete(id);
        if(!response.Success) ProcessError(response);
        return Ok(response.GetResult<ProductProviderDTO>());
    }
}
