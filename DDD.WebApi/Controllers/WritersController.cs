using DDD.Application.Dtos.MappingDtos.WriterMappingDto.Requests;
using DDD.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DDD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WritersController : ControllerBase
    {
        readonly IWriterService _writerService;
        public WritersController(IWriterService writerService)
        {
            _writerService = writerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _writerService.GetAllIncludingAsync();
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpGet("AllData")]
        public async Task<IActionResult> GetAllForAdmin()
        {
            var data = await _writerService.GetAllIncludingForAdminAsync();
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            var data = await _writerService.GetByIdAsync(id);
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpPost("Create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] WriterCreateRequest request)
        {
            var result = await _writerService.CreateAsync(request);
            if (!result)
                return BadRequest(new { message = "Writer oluşturulamadı." });

            return Ok(new { message = "Writer başarıyla oluşturuldu." });
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int id, [FromForm] WriterUpdateRequest request)
        {
            if (id != request.Id)
                return BadRequest("ID eşleşmiyor.");

            var result = await _writerService.UpdateAsync(request);
            if (!result)
                return BadRequest("Writer güncellenemedi.");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _writerService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}/set-deleted")]
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _writerService.SetDeletedAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPatch("{id}/set-not-deleted")]
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _writerService.SetNotDeletedAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
