using StudentDormitoryManagementSystem.Data.Model.Abstracts;
using StudentDormitoryManagementSystem.Data.Model.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentDormitoryManagementSystem.Data.Model.Models
{

    public class Room : DataModel, IDataModel
    {
        public Room()
        {
        }

        [Required]
        public short Number { get; set; }

        [Required]
        public short Floor { get; set; }

        public RoomType Type { get; set; }

        public virtual ICollection<StudentInfo> Students { get; set; } = new HashSet<StudentInfo>();
    }

    public enum RoomType
    {
        Single,
        Double,
        Shared
    }
}
