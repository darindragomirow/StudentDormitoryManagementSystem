using System;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StudentDormitoryManagementSystem.Contracts.Identity;
using StudentDormitoryManagementSystem.Data.Model.Models;

namespace StudentDormitoryManagementSystem.Identity
{
    public class ApplicationSignInManager : SignInManager<User, String>, IApplicationSignInManager
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }
    }
}