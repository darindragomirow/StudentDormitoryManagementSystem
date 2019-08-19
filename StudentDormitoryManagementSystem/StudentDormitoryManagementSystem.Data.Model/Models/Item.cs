using StudentDormitoryManagementSystem.Data.Model.Abstracts;
using StudentDormitoryManagementSystem.Data.Model.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentDormitoryManagementSystem.Data.Model.Models
{
    public class Item : DataModel, IDataModel
    {
        public Item()
        {
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Material { get; set; }

        public string Model { get; set;}

        public State State { get; set; }

        public Size Size { get; set; }

        [Required]
        [ForeignKey("CategoryId")]
        public virtual ItemCategory ItemCategory { get; set; }
        public Guid? CategoryId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
        public Guid? RoomId { get; set; }
        
        public DateTime DateRegistered { get; set; }

        public DateTime ExpirationDate { get; set; }
    }

    public enum Size
    {
        Small,
        Medium,
        Large
    }

    public enum State
    {
        New,
        Well,
        Damaged
    }
}
