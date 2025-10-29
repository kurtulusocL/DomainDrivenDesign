using System.ComponentModel.DataAnnotations;

namespace DDD.Application.Dtos.AuthDtos
{
    public class RegisterDto
    {
        public string NameSurname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string  PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords Are Not Same")]
        public string ConfirmPassword { get; set; }
    }
}
