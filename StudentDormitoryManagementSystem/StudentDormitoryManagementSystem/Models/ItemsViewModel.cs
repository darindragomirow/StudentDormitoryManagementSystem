using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDormitoryManagementSystem.Models
{
    public class ItemsViewModel
    {
        public ItemsViewModel()
        {
            this.AvailableItems = new List<ItemViewModel>();
        }

        public short RoomNumber { get; set; }
        public List<ItemViewModel> AvailableItems { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}