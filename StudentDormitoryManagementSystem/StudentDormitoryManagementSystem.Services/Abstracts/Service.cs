using System.Linq;
using StudentDormitoryManagementSystem.Data.Model.Contracts;
using StudentDormitoryManagementSystem.Data.Repositories;
using StudentDormitoryManagementSystem.Data.SaveContext;
using StudentDormitoryManagementSystem.Services.Contracts;

namespace StudentDormitoryManagementSystem.Services.Abstracts
{
    public abstract class Service<T> : IService<T> where T : class, IDeletable
    {
        protected readonly IEfRepository<T> Repo;
        protected readonly ISaveContext Context;

        protected Service(IEfRepository<T> repo, ISaveContext context)
        {
            this.Repo = repo;
            this.Context = context;
        }

        public IQueryable<T> GetAll()
        {
            return this.Repo.All;
        }

        public IQueryable<T> GetAllAndDeleted()
        {
            return this.Repo.AllAndDeleted;
        }

        public void Add(T obj)
        {
            this.Repo.Add(obj);
            Context.Commit();
        }

        public void Update(T obj)
        {
            this.Repo.Update(obj);
            this.Context.Commit();
        }

        public void Delete(T obj)
        {
            this.Repo.Delete(obj);
            this.Context.Commit();
        }
    }
}
