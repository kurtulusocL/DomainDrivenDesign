using DDD.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DDD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExceptionLoggersController : ControllerBase
    {
        readonly IExceptionLoggerService _loggerService;
        public ExceptionLoggersController(IExceptionLoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _loggerService.GetAllAsync();
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpPost("AllData")]
        public async Task<IActionResult> GetAllForAdmin()
        {
            var data = await _loggerService.GetAllForAdminAsync();
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetById(int? id)
        {
            var data=await _loggerService.GetByIdAsync(id);
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _loggerService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}/set-deleted")]
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _loggerService.SetDeletedAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPatch("{id}/set-not-deleted")]
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _loggerService.SetNotDeletedAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
