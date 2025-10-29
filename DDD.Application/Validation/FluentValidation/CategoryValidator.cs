using DDD.Domain.Entities;
using FluentValidation;

namespace DDD.Application.Validation.FluentValidation
{
    public class CategoryValidator:AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name can not be null");
        }
    }
}
