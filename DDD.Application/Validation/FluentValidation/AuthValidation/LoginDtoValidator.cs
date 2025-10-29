using DDD.Application.Dtos.AuthDtos;
using FluentValidation;

namespace DDD.Application.Validation.FluentValidation.AuthValidation
{
    public class LoginDtoValidator:AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("Email address can not be null and must be valid");
            RuleFor(i => i.Password).NotEmpty().MinimumLength(8).WithMessage("Password can not be null and must be minimum 8 characters");
        }
    }
}
