using StudentDormitoryManagementSystem.Data.Model.Models;
using StudentDormitoryManagementSystem.Data.Repositories;
using StudentDormitoryManagementSystem.Data.SaveContext;
using StudentDormitoryManagementSystem.Services.Abstracts;
using StudentDormitoryManagementSystem.Services.Contracts;

namespace StudentDormitoryManagementSystem.Services
{
    public class InventoriesService : Service<Inventory>, IInventoriesService
    {
        public InventoriesService(IEfRepository<Inventory> repo, ISaveContext context) : base(repo, context)
        {
        }
    }
}
