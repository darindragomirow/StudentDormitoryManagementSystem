using StudentDormitoryManagementSystem.Data.Model.Models;
using StudentDormitoryManagementSystem.Data.Repositories;
using StudentDormitoryManagementSystem.Data.SaveContext;
using StudentDormitoryManagementSystem.Services.Abstracts;
using StudentDormitoryManagementSystem.Services.Contracts;

namespace StudentDormitoryManagementSystem.Services
{
    public class StudentsService : Service<StudentInfo>, IStudentsService
    {
        public StudentsService(IEfRepository<StudentInfo> studentsRepo, ISaveContext context) : base(studentsRepo, context)
        {
        }
    }
}
