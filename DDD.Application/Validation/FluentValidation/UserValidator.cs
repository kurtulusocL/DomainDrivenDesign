using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD.Domain.Entities.EntityFramework.AppUser;
using FluentValidation;

namespace DDD.Application.Validation.FluentValidation
{
    public class UserValidator:AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(i => i.NameSurname).NotEmpty().WithMessage("Name Surname can not be null");
            RuleFor(i => i.PhoneNumber).NotEmpty().WithMessage("Phone Number can not be null");
            RuleFor(i => i.Email).EmailAddress().NotEmpty().WithMessage("Email address can not be null and must be valid email");
            RuleFor(i => i.UserName).NotEmpty().WithMessage("Username can not be null");
        }
    }
}
