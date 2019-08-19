using StudentDormitoryManagementSystem.Data.Model.Models;
using StudentDormitoryManagementSystem.Data.Repositories;
using StudentDormitoryManagementSystem.Data.SaveContext;
using System.Linq;
using StudentDormitoryManagementSystem.Services.Abstracts;
using StudentDormitoryManagementSystem.Services.Contracts;

namespace StudentDormitoryManagementSystem.Services
{
    public class ItemsService : Service<Item>, IItemsService
    {
        public ItemsService(IEfRepository<Item> repo, ISaveContext context) : base(repo, context)
        {
        }
        
        public IQueryable<Item> GetFiltered(string searchTerm)
        {
            if (searchTerm == "")
            {
                return this.Repo.All;
            }

            return this.Repo.All
                .Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
        }

    }
}
