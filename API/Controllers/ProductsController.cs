using Microsoft.AspNetCore.Mvc;
using Pharmacy.Services;

namespace Pharmacy.Presentation.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductsController: ControllerBase
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService) =>
        _productService = productService;

    [HttpGet]
    public IActionResult Get() => Ok(_productService.GetAll());
}
