using DDD.Application.Dtos.MappingDtos.ArticleMappingDto;
using FluentValidation;

namespace DDD.Application.Validation.FluentValidation
{
    public class ArticleDtoValidator:AbstractValidator<ArticleDto>
    {
        public ArticleDtoValidator()
        {
            RuleFor(i => i.Title).NotEmpty().WithMessage("Title can not be null");
            RuleFor(i => i.Subtitle).NotEmpty().WithMessage("Subtitle can not be null");
            RuleFor(i => i.Description).NotEmpty().WithMessage("Description can not be null");
            RuleFor(i => i.ImageUrl).NotEmpty().WithMessage("ImageUrl can not be null");
            RuleFor(i => i.CategoryId).NotEmpty().WithMessage("CategoryId can not be null");
            RuleFor(i => i.WriterId).NotEmpty().WithMessage("WriterId can not be null");
        }
    }
}
