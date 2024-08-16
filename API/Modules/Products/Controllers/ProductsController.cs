using Microsoft.AspNetCore.Mvc;
using Pharmacy.Presentation.Generics;
using Pharmacy.Application.Utilities;
using Pharmacy.Services;
using Pharmacy.Shared.Generics;
using Pharmacy.Shared.Modules.Products.DTOs;

namespace Pharmacy.Presentation.Controllers;


[ApiController]
public class ProductsController : GenericController
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService) =>
        _productService = productService;

    [HttpGet]
    public IActionResult Get() =>
        Ok(
            _productService.GetAll()
            .GetResult<IEnumerable<ProductDTO>>()
        );

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        BaseResponse response = _productService.GetById(id);
        if(!response.Success) ProcessError(response);
        return Ok(response.GetResult<ProductDTO>());
    }

    [HttpPost]
    public IActionResult Create(ProductCreateDTO product) =>
        Ok(
            _productService.Create(product).GetResult<ProductDTO>()
        );

    [HttpPut]
    public IActionResult Update(Guid id, ProductCreateDTO product)
    {
        BaseResponse response = _productService.Update(id, product);
        if(!response.Success) ProcessError(response);
        return Ok(response.GetResult<ProductDTO>());
    }

    [HttpDelete]
    public IActionResult Delete(Guid id)
    {
        BaseResponse response = _productService.Delete(id);
        if(!response.Success) ProcessError(response);
        return Ok(response.GetResult<ProductDTO>());
    }
}
