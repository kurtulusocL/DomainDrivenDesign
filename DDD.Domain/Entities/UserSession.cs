using DDD.Domain.Entities.EntityFramework;
using DDD.Domain.Entities.EntityFramework.AppUser;

namespace DDD.Domain.Entities
{
    public class UserSession : BaseEntity
    {
        public string Username { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime? LogoutDate { get; set; }
        public bool IsOnline { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
