using DDD.Domain.Entities.EntityFramework;

namespace DDD.Domain.Entities
{
    public class ExceptionLogger : BaseEntity
    {
        public string ExceptionMessage { get; set; }
        public string ControllerName { get; set; }
        public string ExceptionStackTrace { get; set; }
    }
}
