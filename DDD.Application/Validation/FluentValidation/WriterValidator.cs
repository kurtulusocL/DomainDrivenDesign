using DDD.Domain.Entities;
using FluentValidation;

namespace DDD.Application.Validation.FluentValidation
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(i => i.NameSurname).NotEmpty().WithMessage("Name Surname can not ben empty");
            RuleFor(i => i.Title).NotEmpty().WithMessage("Title can not be null");
            RuleFor(i => i.ImageUrl).NotEmpty().WithMessage("Image Url can not be null");
        }
    }
}
