using StudentDormitoryManagementSystem.Data.Model.Models;
using StudentDormitoryManagementSystem.Data.Repositories;
using StudentDormitoryManagementSystem.Data.SaveContext;
using StudentDormitoryManagementSystem.Services.Abstracts;
using StudentDormitoryManagementSystem.Services.Contracts;

namespace StudentDormitoryManagementSystem.Services
{
    public class UsersService : Service<User>, IUsersService
    {
        public UsersService(IEfRepository<User> repo, ISaveContext context) : base(repo, context)
        {
        }
    }
}
