using Microsoft.AspNetCore.Mvc;
using Pharmacy.Presentation.Extensions;
using Pharmacy.Presentation.Generics;
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
    public IActionResult Get(int id)
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
    public IActionResult Update(int id, ProductCreateDTO product)
    {
        BaseResponse response = _productService.Update(id, product);
        if(!response.Success) ProcessError(response);
        return Ok(response.GetResult<ProductDTO>());
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        BaseResponse response = _productService.Remove(id);
        if(!response.Success) ProcessError(response);
        return Ok(response.GetResult<ProductDTO>());
    }
}
