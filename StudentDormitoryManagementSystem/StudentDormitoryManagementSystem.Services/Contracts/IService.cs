using System.Linq;

namespace StudentDormitoryManagementSystem.Services.Contracts
{
    public interface IService<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAllAndDeleted();
        void Add(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}
