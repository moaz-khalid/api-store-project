using api_course_project.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Repository.Data.Contexts;

namespace api_course_project.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;

        }

        [HttpGet("notfound")]
        public async Task<IActionResult> GetNotFoundRequestError()
        {
            var brand =await _context.Brands.FindAsync(100);

            if(brand is null) 
            {
                return NotFound(new ApiErrorResponse(404) );    //404 error
            }

            return Ok(brand);
        }


        [HttpGet("servererror")]
        public async Task<IActionResult> GetServerError()
        {
            var brand = await _context.Brands.FindAsync(100);

            var brandToString = brand.ToString();    //null reference exception

            return Ok(brand);
        }


        [HttpGet("badrequest")]
        public async Task<IActionResult> GetBadRequestError()
        {
            return BadRequest(new ApiErrorResponse(400));    //400 error
        }


        [HttpGet("badrequest/{id}")]
        public async Task<IActionResult> GetBadRequestError(int id)
        {



            return Ok();   //validation error
        }

        [HttpGet("unauthorized")]
        public async Task<IActionResult> GetUnauthorizedError()
        {
            return Unauthorized(new ApiErrorResponse(401));    //401 error
        }





    }
}
