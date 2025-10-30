using DDD.Application.Dtos.MappingDtos.UserMappingDto;
using FluentValidation;

namespace DDD.Application.Validation.FluentValidation
{
    public class UserDtoValidator:AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(i => i.NameSurname).NotEmpty().WithMessage("Name Surname can not be null");
            RuleFor(i => i.PhoneNumber).NotEmpty().WithMessage("Phone Number can not be null");
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("Email address can not be null and must be valid email");
            RuleFor(i => i.UserName).NotEmpty().WithMessage("Username can not be null");
        }
    }
}
