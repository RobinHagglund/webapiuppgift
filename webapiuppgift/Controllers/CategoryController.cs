using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapiuppgift.Services;

namespace webapiuppgift.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return new OkObjectResult(await _service.GetAllCategories());
        }
    }
}
