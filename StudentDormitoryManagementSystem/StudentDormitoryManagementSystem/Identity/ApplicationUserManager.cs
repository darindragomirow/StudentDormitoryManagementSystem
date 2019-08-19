using System;
using Microsoft.AspNet.Identity;
using StudentDormitoryManagementSystem.Contracts.Identity;
using StudentDormitoryManagementSystem.Data.Model.Models;

namespace StudentDormitoryManagementSystem.Identity
{
    public class ApplicationUserManager : UserManager<User>, IApplicationUserManager
    {
        public ApplicationUserManager(IUserStore<User> store)
            : base(store)
        {
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<User>(this)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = false,
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;
        }
    }
}