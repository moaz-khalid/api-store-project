using api_course_project.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_course_project.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : BaseApiController
    {

        //not found endpoint handling
        public IActionResult Error(int code) 
        {
        return NotFound(new ApiErrorResponse (StatusCodes.Status404NotFound,"not found endpoint!"));
        }
    }
}
