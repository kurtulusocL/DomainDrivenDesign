using DDD.Application.Dtos.MappingDtos.WriterMappingDto;
using FluentValidation;

namespace DDD.Application.Validation.FluentValidation
{
    public class WriterDtoValidator : AbstractValidator<WriterDto>
    {
        public WriterDtoValidator()
        {
            RuleFor(i => i.NameSurname).NotEmpty().WithMessage("Name Surname can not ben empty");
            RuleFor(i => i.Title).NotEmpty().WithMessage("Title can not be null");
            RuleFor(i => i.ImageUrl).NotEmpty().WithMessage("Image Url can not be null");
        }
    }
}
