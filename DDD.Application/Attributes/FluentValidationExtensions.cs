using DDD.Application.Validation.FluentValidation;
using DDD.Application.Validation.FluentValidation.AuthValidation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Application.Attributes
{
    public static class FluentValidationExtensions
    {
        public static void AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<ArticleValidator>();
            services.AddValidatorsFromAssemblyContaining<CategoryValidator>();
            services.AddValidatorsFromAssemblyContaining<WriterValidator>();
            services.AddValidatorsFromAssemblyContaining<RoleValidator>();
            services.AddValidatorsFromAssemblyContaining<UserValidator>();

            services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<ConfirmCodeDtoValidator>();

            services.AddFluentValidationAutoValidation();
        }
    }
}
