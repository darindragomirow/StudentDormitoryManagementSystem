using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using StudentDormitoryManagementSystem.Data.Model.Models;

namespace StudentDormitoryManagementSystem.Contracts.Identity
{
    public interface IApplicationUserManager
    {
        Task<User> FindByIdAsync(string userId);

        Task<IdentityResult> CreateAsync(User user, string password);

        Task<string> GetEmailAsync(string userId);

        Task<IdentityResult> SetEmailAsync(string userId, string email);

        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);

        IQueryable<User> Users { get; }
    }
}
