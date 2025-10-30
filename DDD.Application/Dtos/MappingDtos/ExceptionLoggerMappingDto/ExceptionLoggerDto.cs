
namespace DDD.Application.Dtos.MappingDtos.ExceptionLoggerMappingDto
{
    public class ExceptionLoggerDto
    {
        public int Id { get; set; }
        public string ExceptionMessage { get; set; }
        public string ControllerName { get; set; }
        public string ExceptionStackTrace { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; } = DateTime.UtcNow;
        public DateTime CreatedDate { get; set; }
    }
}
