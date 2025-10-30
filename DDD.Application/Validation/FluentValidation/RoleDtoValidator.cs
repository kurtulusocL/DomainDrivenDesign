using DDD.Application.Dtos.MappingDtos.RoleMappingDto;
using FluentValidation;

namespace DDD.Application.Validation.FluentValidation
{
    public class RoleDtoValidator:AbstractValidator<RoleDto>
    {
        public RoleDtoValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name can not be null");
        }
    }
}
