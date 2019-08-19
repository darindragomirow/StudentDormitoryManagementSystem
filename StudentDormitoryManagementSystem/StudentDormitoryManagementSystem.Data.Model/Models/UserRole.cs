using System.ComponentModel.DataAnnotations;

namespace StudentDormitoryManagementSystem.Data.Model.Models
{
    public class UserRole 
    {
        public UserRole()
        {
        }

        [Key]
        public int RoleId { get; set; }

        public RoleType Type { get; set; }
    }

    public enum RoleType
    {
        Student,
        Host,
        Employee
    }
}
