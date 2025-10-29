using DDD.Domain.Entities.EntityFramework.AppUser;
using FluentValidation;

namespace DDD.Application.Validation.FluentValidation
{
    public class RoleValidator:AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name can not be null");
        }
    }
}
