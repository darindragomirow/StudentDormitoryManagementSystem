using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentDormitoryManagementSystem.Data.Model.Abstracts;
using StudentDormitoryManagementSystem.Data.Model.Contracts;

namespace StudentDormitoryManagementSystem.Data.Model.Models
{
    public class StudentInfo : DataModel, IDataModel
    {
        public StudentInfo()
        {
            if (this.Inventory == null)
            {
                this.Inventory = new Inventory();
            }
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SecondName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string University { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string BirthPlace { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public short Years { get; set; }

        [Required]
        public string Nationality { get; set; }

        [Required]
        public string EGN { get; set; }

        [Required]
        public string PersonalCardNumber { get; set; }

        [Required]
        public string Speciality { get; set; }

        [Required]
        public short Course { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
        public Guid? RoomId { get; set; }

        [Required]
        public virtual Inventory Inventory { get; set; }
        public Guid? InventoryId { get; set; }
    }
}
