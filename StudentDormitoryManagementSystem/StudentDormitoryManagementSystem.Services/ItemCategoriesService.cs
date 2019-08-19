using StudentDormitoryManagementSystem.Data.Model.Models;
using StudentDormitoryManagementSystem.Data.Repositories;
using StudentDormitoryManagementSystem.Data.SaveContext;
using StudentDormitoryManagementSystem.Services.Abstracts;
using StudentDormitoryManagementSystem.Services.Contracts;

namespace StudentDormitoryManagementSystem.Services
{
    public class ItemCategoriesService : Service<ItemCategory>, IItemCategoriesService
    {
        public ItemCategoriesService(IEfRepository<ItemCategory> repo, ISaveContext context) : base(repo, context)
        {
        }
    }
}
