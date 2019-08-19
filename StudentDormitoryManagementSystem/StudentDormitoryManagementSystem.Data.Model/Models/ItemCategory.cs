using System.Collections.Generic;
using StudentDormitoryManagementSystem.Data.Model.Abstracts;
using StudentDormitoryManagementSystem.Data.Model.Contracts;

namespace StudentDormitoryManagementSystem.Data.Model.Models
{
    public class ItemCategory : DataModel, IDataModel
    {
        public ItemCategory()
        {
        }

        public string CategoryName { get; set; }

        public virtual ICollection<Item> Items { get; set; } = new HashSet<Item>();
    }
}
