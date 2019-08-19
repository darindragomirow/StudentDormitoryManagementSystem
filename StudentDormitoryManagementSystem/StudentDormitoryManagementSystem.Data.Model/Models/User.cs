using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentDormitoryManagementSystem.Data.Model.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StudentDormitoryManagementSystem.Data.Model.Models
{
    public class User : IdentityUser, IDeletable
    {
        public User()
        {
            
        }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }

        //[DataType(DataType.DateTime)]
        //public DateTime? DeletedOn { get; set; }

        //[DataType(DataType.DateTime)]
        //public DateTime? CreatedOn { get; set; }

        //[DataType(DataType.DateTime)]
        //public DateTime? ModifiedOn { get; set; }

        //[ForeignKey("UserRoleId")]
        //public virtual UserRole UserRole { get; set; }
        //public int UserRoleId { get; set; }

        [ForeignKey("StudentInfoId")]
        public virtual StudentInfo StudentInfo { get; set; }
        public Guid? StudentInfoId { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
