using DDD.Application.Attributes;
using DDD.Application.Services.Abstract;
using DDD.Application.Services.Concrete;
using DDD.Domain.Repositories.Abstract;
using DDD.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Application.DependencyResolver.DependencyInjection
{
    public static class DependencyContainer
    {
        public static void DependencyService(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthManager>();

            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleService, ArticleService>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IExceptionLoggerRepository, ExceptionLoggerRepository>();
            services.AddScoped<IExceptionLoggerService, ExceptionLoggerService>();

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserSessionRepository, UserSessionRepository>();
            services.AddScoped<IUserSessionService, UserSessionService>();

            services.AddScoped<IWriterRepository, WriterRepository>();
            services.AddScoped<IWriterService, WriterService>();

            services.AddFluentValidationServices();
        }
    }
}
