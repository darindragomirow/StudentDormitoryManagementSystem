using System;
using System.Web;
using StudentDormitoryManagementSystem.Data.Model.Abstracts;

namespace StudentDormitoryManagementSystem.Data.Model.Models
{
    public class Image : DataModel
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public Guid? ItemId { get; set; }
    }
}
