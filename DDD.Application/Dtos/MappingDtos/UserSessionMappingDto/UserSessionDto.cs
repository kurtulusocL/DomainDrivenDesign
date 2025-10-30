
namespace DDD.Application.Dtos.MappingDtos.UserSessionMappingDto
{
    public class UserSessionDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime? LogoutDate { get; set; }
        public bool IsOnline { get; set; }

        public string UserId { get; set; }
    }
}
