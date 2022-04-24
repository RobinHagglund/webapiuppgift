using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapiuppgift.Models.Product;
using webapiuppgift.Services;

namespace webapiuppgift.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, User")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _service.GetAll());
        }
        
        [HttpPost("{artnr}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(string artnr, ProductUpdateRequest request)
        {
            var item = await _service.UpdateProductAsync(artnr, request);
            if (item != null)
                return new OkObjectResult(item);

            return new BadRequestResult();
        }

        //Create Product       
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Product product)
        {
            var item = await _service.CreateAsync(product);
            if (item != null)
                return new OkObjectResult(item);

            return new BadRequestResult();
        }
        
        //Delete product
        [HttpDelete("{artnr}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string artnr)
        {
            if (await _service.DeleteAsync(artnr))
                return new OkResult();
            return new NotFoundResult();
        }
    }
}
