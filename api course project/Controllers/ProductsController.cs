using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Dtos.Products;
using Store.Core.Helper;
using Store.Core.Services.Contract;
using Store.Core.Specifications.Products;


namespace api_course_project.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _ProductService;

        public ProductsController(IProductService productService)
        {
            _ProductService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductSpecParams productSpec)
        {
            var resault = await _ProductService.GetAllProductsAsync(productSpec);

            return Ok(resault);
        }


        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var resault = await _ProductService.GetAllBrandsAsync();

            return Ok(resault);
        }


        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var resault = await _ProductService.GetAllTypesAsync();

            return Ok(resault);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int? id)
        {
            if (id == null) return BadRequest("Invalid product ID");

            var resault = await _ProductService.GetProductByIdAsync(id.Value);

            if (resault == null) return NotFound($"Product with ID: {id} not found");

            return Ok(resault);
        }


    }
}
