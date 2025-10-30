using DDD.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DDD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var data = await _userService.GetAllIncludingAsync();
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpGet("AllData")]
        public async Task<IActionResult> GetAllForAdmin()
        {
            var data = await _userService.GetAllIncludingForAdminAsync();
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var data = await _userService.GetByIdAsync(id);
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}/set-deleted")]
        public async Task<IActionResult> SetDeleted(string id)
        {
            var result = await _userService.SetDeletedAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPatch("{id}/set-not-deleted")]
        public async Task<IActionResult> SetNotDeleted(string id)
        {
            var result = await _userService.SetNotDeletedAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
