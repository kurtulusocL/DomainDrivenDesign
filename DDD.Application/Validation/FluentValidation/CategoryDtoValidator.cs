using DDD.Application.Dtos.MappingDtos.CategoryMappingDtos;
using FluentValidation;

namespace DDD.Application.Validation.FluentValidation
{
    public class CategoryDtoValidator:AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("Name can not be null");
        }
    }
}
