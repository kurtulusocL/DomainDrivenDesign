
namespace DDD.Application.Dtos.MappingDtos.ExceptionLoggerMappingDto
{
    public class ExceptionLoggerCreateDto
    {
        public string ExceptionMessage { get; set; }
        public string ControllerName { get; set; }
        public string ExceptionStackTrace { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
