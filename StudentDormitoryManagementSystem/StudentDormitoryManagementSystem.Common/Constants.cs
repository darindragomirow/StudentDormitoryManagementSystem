using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDormitoryManagementSystem.Common
{
    public class Constants
    {
        public const int DefaultPageSize = 10;
        public const string AdminRole = "Admin";
        public const string AdminEmail = "admin@sdms.bg";
        public const short KitchenRoomNumber = 101;
        public const short LaundryRoomNumber = 102;
        public const short GamingRoomNumber = 103;
        public const short LibraryRoomNumber = 104;
        public const string LocalServerPath = "http://localhost/StudentDormitoryManagementSystem";
        public const string UploadedImagesPath = "/Content/Images/Uploaded/";
        public const string NotFoundImagePath = LocalServerPath + "/Content/Images/No-image-found.jpg";
        public const string AlertImagePath = LocalServerPath + "/Content/Images/alert.jpg";
        public const string ItemDetailsURL = "http://127.0.0.1/studentdormitorymanagementsystem/myinventory/getitemdetails/";
    }
}
