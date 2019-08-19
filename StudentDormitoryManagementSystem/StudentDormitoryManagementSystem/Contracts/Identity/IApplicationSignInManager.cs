using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using StudentDormitoryManagementSystem.Data.Model.Models;

namespace StudentDormitoryManagementSystem.Contracts.Identity
{
    public interface IApplicationSignInManager
    {
        Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout);

        Task SignInAsync(User user, bool isPersistent, bool rememberBrowser);
    }
}
