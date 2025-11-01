using DDD.Application.Dtos.AuthDtos;
using FluentValidation;

namespace DDD.Application.Validation.FluentValidation.AuthValidation
{
    public class UserRegisterDtoValidator:AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(i => i.NameSurname).NotEmpty().WithMessage("Name Surname can not be null.");
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("Email address can not be null and must be valid");
            RuleFor(i => i.Password).NotEmpty().MinimumLength(8).WithMessage("Password can not be null and must be minimum 8 characters");
            RuleFor(i => i.ConfirmPassword).NotEmpty().MinimumLength(8).WithMessage("Confirmed Password can not be null and must be minimum 8 characters");
        }
    }
}
