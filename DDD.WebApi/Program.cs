using DDD.Application.DependencyResolver.DependencyInjection;
using DDD.Application.Mapping.ArticleMapping;
using DDD.Application.Mapping.CategoryMapping;
using DDD.Application.Mapping.ExceptionLoggerMapping;
using DDD.Application.Mapping.RoleMapping;
using DDD.Application.Mapping.UserMapping;
using DDD.Application.Mapping.UserSessionMapping;
using DDD.Application.Mapping.WriterMapping;
using DDD.Domain.Entities.EntityFramework.AppUser;
using DDD.Infrastructure.EntityFramework.Context.Mssql;
using DDD.Middleware;
using DDD.WebApi.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.DependencyService();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ArticleProfile>();
    cfg.AddProfile<CategoryProfile>();
    cfg.AddProfile<ExceptionLoggerProfile>();
    cfg.AddProfile<RoleProfile>();
    cfg.AddProfile<UserProfile>();
    cfg.AddProfile<UserSessionProfile>();
    cfg.AddProfile<WriterProfile>();
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlServerOptions => sqlServerOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

builder.Services.AddIdentity<User, Role>(opt =>
{
    opt.Password.RequireDigit = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequiredLength = 8;
    opt.Lockout.AllowedForNewUsers = true;
    opt.User.RequireUniqueEmail = true;
    opt.Lockout.MaxFailedAccessAttempts = 4;

}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

ServiceProviderHelper.ServiceProvider = app.Services;
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapGet("/", () => Results.Redirect("/swagger"));
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<StatusCodeHandlerMiddleware>();
app.MapStaticAssets();

app.Run();