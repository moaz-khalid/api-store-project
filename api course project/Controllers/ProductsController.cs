using api_course_project.Attributes;
using api_course_project.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Dtos.Products;
using Store.Core.Helper;
using Store.Core.Services.Contract;
using Store.Core.Specifications.Products;


namespace api_course_project.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IProductService _ProductService;

        public ProductsController(IProductService productService)
        {
            _ProductService = productService; 
        }




        [ProducesResponseType(typeof(PaginationResponse<ProductDto>), StatusCodes.Status200OK)]
        [HttpGet]
        [Cached(100)]
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProducts([FromQuery] ProductSpecParams productSpec)
        {
            var resault = await _ProductService.GetAllProductsAsync(productSpec);

            return Ok(resault);
        }

        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllBrands()
        {
            var resault = await _ProductService.GetAllBrandsAsync();

            return Ok(resault);
        }

        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllTypes()
        {
            var resault = await _ProductService.GetAllTypesAsync();

            return Ok(resault);
        }


        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int? id)
        {
            if (id == null) return BadRequest(new ApiErrorResponse(400));

            var resault = await _ProductService.GetProductByIdAsync(id.Value);

            if (resault == null) return NotFound(new ApiErrorResponse(404, $"Product with ID: {id} not found"));

            return Ok(resault);
        }


    }
}
