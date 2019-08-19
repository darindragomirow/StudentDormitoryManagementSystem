using System;
using System.ComponentModel.DataAnnotations;

namespace StudentDormitoryManagementSystem.Data.Model.Contracts
{
    public interface IDataModel
    {
        Guid Id { get; set; }

        bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]
        DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        DateTime? ModifiedOn { get; set; }
    }
}
