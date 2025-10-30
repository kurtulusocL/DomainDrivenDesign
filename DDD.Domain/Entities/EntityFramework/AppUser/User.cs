using DDD.Domain.Entities.EntityFramework.Abstract;
using Microsoft.AspNetCore.Identity;

namespace DDD.Domain.Entities.EntityFramework.AppUser
{
    public class User : IdentityUser, IEntity, IDeletable
    {
        public string NameSurname { get; set; }
        public string PhoneNumber { get; set; }
        public int? ConfirmCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual ICollection<UserSession> UserSessions  { get; set; }

        public void SetCreatedDate()
        {
            CreatedDate = DateTime.UtcNow;
        }

        public void SetDeleted()
        {
            IsDeleted = false;
        }
        public User()
        {
            PhoneNumberConfirmed = true;
            EmailConfirmed = true;
            SetCreatedDate();
            SetDeleted();
        }
    }
}
