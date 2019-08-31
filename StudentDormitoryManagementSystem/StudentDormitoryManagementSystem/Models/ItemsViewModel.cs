using StudentDormitoryManagementSystem.Data.Model.Models;
using System.Collections.Generic;

namespace StudentDormitoryManagementSystem.Models
{
    public class ItemsViewModel
    {
        public ItemsViewModel()
        {
            this.AvailableItems = new List<ItemViewModel>();
        }

        public short RoomNumber { get; set; }
        public string RoomType { get; set; }
        public User Owner { get; set; }
        public List<ItemViewModel> AvailableItems { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}