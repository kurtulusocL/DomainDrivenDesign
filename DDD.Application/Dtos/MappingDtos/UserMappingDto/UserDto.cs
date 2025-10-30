

namespace DDD.Application.Dtos.MappingDtos.UserMappingDto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public int? ConfirmCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
        public UserDto()
        {
            EmailConfirmed = true;
            PhoneNumberConfirmed = true;
        }
    }
}
