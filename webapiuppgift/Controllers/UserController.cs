using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapiuppgift.Models.User;
using webapiuppgift.Services;
using webapiuppgift.Data;
using Microsoft.EntityFrameworkCore;

namespace webapiuppgift.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatebaseContext _db;
        private readonly IUserService _service;

        //public UserController(IUserService service)
        //{
        //    _service = service;
        //}

        public UserController(DatebaseContext db, IUserService service)
        {
            _db = db;
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _service.GetAll());
        }

        [HttpGet("{Email}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserByEmail(string Email)
        {
            //return new OkObjectResult(await _db.Users.FindAsync(id));
            return new OkObjectResult(await _db.Users.FirstOrDefaultAsync(x => x.Email == Email));
        }

        [HttpPut("id")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateRequest request)
        {
            var item = await _service.UpdateUserAsync(id, request);
            if (item != null)
                return new OkObjectResult(item);

            return new BadRequestResult();
        }

        [HttpDelete("id")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var item = await _service.DeleteAsync(id);
            if (item != null)
                return new OkObjectResult(item);

            return new BadRequestResult();
        }
    }
}
