using DDD.Application.Dtos.AuthDtos;
using DDD.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DDD.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        readonly IAuthService _authService;
        public AccountsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _authService.LoginAsync(model);
            if (!result)
                return BadRequest(new { message = "Login olunamadı." });

            return Ok(new { message = "Login olundu." });
        }

        [HttpPost("admin-register")]
        public async Task<IActionResult> AdminRegister(RegisterDto model)
        {
            var result = await _authService.AdminRegisterAsync(model);
            if (!result)
                return BadRequest(new { message = "Admin Register olunamadı." });

            return Ok(new { message = "Admin Register olundu." });
        }

        [HttpPost("user-register")]
        public async Task<IActionResult> UserRegister(RegisterDto model)
        {
            var result = await _authService.RegisterAsync(model);
            if (result)
            {
                return Ok(new
                {
                    success = true,
                    message = "Registration successful. Activation code sent to your email.",
                    email = model.Email
                });
            }

            return BadRequest(new
            {
                success = false,
                message = "Registration failed"
            });
        }

        [HttpPost("confirm-mail")]
        public async Task<IActionResult> ConfirmMail([FromBody] ConfirmCodeDto model)
        {
            var result = await _authService.ConfirmMailAsync(model);
            if (result)
            {
                return Ok(new { success = true, message = "Email confirmed successfully. You can now login." });
            }
            return BadRequest(new { success = false, message = "Email confirmation failed" });
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authService.LogoutAsync();
            if (!result)
                return BadRequest(new { message = "Logout olunamadı." });

            return Ok(new { message = "Logout olundu." });
        }
    }
}
