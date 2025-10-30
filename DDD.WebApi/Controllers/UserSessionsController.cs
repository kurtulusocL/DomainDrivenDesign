using DDD.Application.Services.Abstract;
using DDD.Application.Services.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSessionsController : ControllerBase
    {
        readonly IUserSessionService _userSessionService;
        public UserSessionsController(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _userSessionService.GetAllIncludingAsync();
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpGet("OnlineUsers")]
        public async Task<IActionResult> OnlineUserList()
        {
            var data = await _userSessionService.GetAllIncludingByOnlineUserAsync();
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpPost("OfflineUsers")]
        public async Task<IActionResult> OfflineUserList()
        {
            var data = await _userSessionService.GetAllIncludingByOfflineUserAsync();
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> ByUserId(string id)
        {
            var data = await _userSessionService.GetAllIncludingByUserIdAsync(id);
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpGet("AllData")]
        public async Task<IActionResult> GetAllForAdmin()
        {
            var data = await _userSessionService.GetAllIncludingForAdminAsync();
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int? id)
        {
            var data = await _userSessionService.GetByIdAsync(id);
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userSessionService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}/set-deleted")]
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _userSessionService.SetDeletedAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPatch("{id}/set-not-deleted")]
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _userSessionService.SetNotDeletedAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
