using AutoMapper;
using DDD.Application.Dtos.MappingDtos.ExceptionLoggerMappingDto;
using DDD.Application.Services.Abstract;
using DDD.Domain.Entities;
using DDD.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DDD.WebApi.Attributes
{
    public class ExceptionHandlerAttribute: Attribute, IExceptionFilter
    {
        readonly IMapper _mapper;
        public ExceptionHandlerAttribute(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void OnException(ExceptionContext filterContext)
        {
            using var scope = ServiceProviderHelper.ServiceProvider.CreateScope();
            var loggingService = scope.ServiceProvider.GetRequiredService<IExceptionLoggerService>();
            var controllerName = filterContext.RouteData.Values["controller"].ToString();

            var logger = new ExceptionLoggerCreateDto
            {
                ExceptionMessage = filterContext.Exception.Message,
                ExceptionStackTrace = filterContext.Exception.StackTrace,
                ControllerName = controllerName,
                CreatedDate = DateTime.Now
            };

            loggingService.CreateAsync(logger).Wait();
            var result = _mapper.Map<ExceptionLogger>(loggingService); 
            filterContext.ExceptionHandled = true;
        }
    }
}