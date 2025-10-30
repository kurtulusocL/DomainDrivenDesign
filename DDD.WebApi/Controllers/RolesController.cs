using DDD.Application.Dtos.MappingDtos.RoleMappingDto;
using DDD.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DDD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        readonly IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _roleService.GetAllAsync();
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpGet("AllData")]
        public async Task<IActionResult> GetAllForAdmin()
        {
            var data = await _roleService.GetAllForAdminAsync();
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var data = await _roleService.GetByIdAsync(id);
            if (data != null)
                return Ok(data);
            return BadRequest();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] RoleCreateDto entity)
        {
            var result = await _roleService.CreateAsync(entity);
            if (!result)
                return BadRequest(new { message = "Role oluşturulamadı." });

            return Ok(new { message = "Role başarıyla oluşturuldu." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] RoleUpdateDto entity)
        {
            if (id != entity.Id)
                return BadRequest("ID eşleşmiyor.");

            var result = await _roleService.UpdateAsync(entity);
            if (!result)
                return BadRequest("Role güncellenemedi.");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _roleService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}/set-deleted")]
        public async Task<IActionResult> SetDeleted(string id)
        {
            var result = await _roleService.SetDeletedAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPatch("{id}/set-not-deleted")]
        public async Task<IActionResult> SetNotDeleted(string id)
        {
            var result = await _roleService.SetNotDeletedAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
