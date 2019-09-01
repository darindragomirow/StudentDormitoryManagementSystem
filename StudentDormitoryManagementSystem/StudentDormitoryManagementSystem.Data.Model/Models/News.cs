using System;
using StudentDormitoryManagementSystem.Data.Model.Abstracts;

namespace StudentDormitoryManagementSystem.Data.Model.Models
{
    public class News : DataModel
    {
        public string Title { get; set; }
        
        public string Description { get; set; }
    }
}
