using DDD.Domain.Entities.EntityFramework.Abstract;
using Microsoft.AspNetCore.Identity;

namespace DDD.Domain.Entities.EntityFramework.AppUser
{
    public class Role : IdentityRole, IEntity, IDeletable
    {
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public void SetCreatedDate()
        {
            CreatedDate = DateTime.UtcNow;
        }

        public void SetDeleted()
        {
            IsDeleted = false;
        }
    }
}
