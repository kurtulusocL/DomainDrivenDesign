using DDD.Application.Dtos.MappingDtos.ArticleMappingDto.Requests;
using DDD.Application.Services.Abstract;
using DDD.Application.Services.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        readonly IArticleService _articleService;
        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _articleService.GetAllIncludingAsync();
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetAllByCategoryId(int id)
        {
            var data = await _articleService.GetAllIncludingByCategoryIdAsync(id);
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpGet("writer/{writerId}")]
        public async Task<IActionResult> GetAllByWriterId(int id)
        {
            var data = await _articleService.GetAllIncludingByWriterIdAsync(id);
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpGet("AllData")]
        public async Task<IActionResult> GetAllForAdmin()
        {
            var data = await _articleService.GetAllIncludingForAdminAsync();
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            var data = await _articleService.GetByIdAsync(id);
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpPost("Create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ArticleCreateRequest request)
        {
            var result = await _articleService.CreateAsync(request);
            if (!result)
                return BadRequest(new { message = "Article oluşturulamadı." });

            return Ok(new { message = "Article başarıyla oluşturuldu." });
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int id, [FromForm] ArticleUpdateRequest request)
        {
            if (id != request.Id)
                return BadRequest("ID eşleşmiyor.");

            var result = await _articleService.UpdateAsync(request);
            if (!result)
                return BadRequest("Writer güncellenemedi.");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _articleService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}/set-deleted")]
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _articleService.SetDeletedAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPatch("{id}/set-not-deleted")]
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _articleService.SetNotDeletedAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
