using DDD.Application.Dtos.MappingDtos.CategoryMappingDtos;
using DDD.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DDD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _categoryService.GetAllIncludingAsync();
            if (data == null)
                return BadRequest();
            return Ok(data);
        }

        [HttpGet("ForAdmin")]
        public async Task<IActionResult> GetAllForAdmin()
        {
            var data = await _categoryService.GetAllIncludingForAdminAsync();
            if (data == null)
                return BadRequest();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            var data = await _categoryService.GetByIdAsync(id);
            if (data == null)
                return BadRequest();
            return Ok(data);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto entity)
        {
            var result = await _categoryService.CreateAsync(entity);

            if (!result)
                return BadRequest(new { message = "Kategori oluşturulamadı." });

            return Ok(new { message = "Kategori başarıyla oluşturuldu." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDto entity)
        {
            if (id != entity.Id)
                return BadRequest("ID eşleşmiyor.");

            var result = await _categoryService.UpdateAsync(entity);
            if (!result)
                return BadRequest("Kategori güncellenemedi.");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}/set-deleted")]
        public async Task<IActionResult> SetDeleted(int id)
        {
            var result = await _categoryService.SetDeletedAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPatch("{id}/set-not-deleted")]
        public async Task<IActionResult> SetNotDeleted(int id)
        {
            var result = await _categoryService.SetNotDeletedAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
