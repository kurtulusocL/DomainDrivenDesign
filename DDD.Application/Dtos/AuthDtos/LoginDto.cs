using System.ComponentModel.DataAnnotations;

namespace DDD.Application.Dtos.AuthDtos
{
    public class LoginDto
    {
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
