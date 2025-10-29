using DDD.Application.Dtos.AuthDtos;
using FluentValidation;

namespace DDD.Application.Validation.FluentValidation.AuthValidation
{
    public class ConfirmCodeDtoValidator : AbstractValidator<ConfirmCodeDto>
    {
        public ConfirmCodeDtoValidator()
        {
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("Email address can not be empty and must be valid.");
            RuleFor(i => i.ConfirmCode).NotEmpty().WithMessage("Confirm Code can not be empty");
        }
    }
}
