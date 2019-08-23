using StudentDormitoryManagementSystem.Data.Model.Abstracts;
using StudentDormitoryManagementSystem.Data.Model.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentDormitoryManagementSystem.Data.Model.Models
{
    public class Inventory : DataModel, IDataModel
    {
        public Inventory()
        {
        }

        public InventoryType Type { get; set; }

        public bool Approved { get; set; }

        public DateTime? LastDateModified { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
        public Guid? RoomId { get; set; }

        public virtual ICollection<Item> Items { get; set; } = new HashSet<Item>();
    }

    public enum InventoryType
    {
        Personal,
        Shared
    }
}
