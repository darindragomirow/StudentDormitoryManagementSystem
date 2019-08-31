using StudentDormitoryManagementSystem.Data.Model.Abstracts;
using StudentDormitoryManagementSystem.Data.Model.Contracts;
using System;

namespace StudentDormitoryManagementSystem.Data.Model.Models
{
    public class ItemReservation : DataModel, IDataModel
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Subject { get; set; }
        public bool IsAllDay { get; set; }
        public string EndTimezone { get; set; }
        public string StartTimezone { get; set; }
        public string RecurrenceRule { get; set; }
        public int? RecurrenceID { get; set; }
        public string RecurrenceException { get; set; }
        public Guid? ItemId { get; set; }
    }
}
