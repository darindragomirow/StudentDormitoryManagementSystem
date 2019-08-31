using StudentDormitoryManagementSystem.Data.Model.Abstracts;
using System;

namespace StudentDormitoryManagementSystem.Data.Model.Models
{
    public class Breakdown : DataModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public short? RoomNumber { get; set; }

        public string Sender { get; set; }

        public Severity Severity { get; set; }

        public bool Acknowledged { get; set; }
    }

    public enum Severity
    {
        Low,
        Medium,
        High
    }
}
