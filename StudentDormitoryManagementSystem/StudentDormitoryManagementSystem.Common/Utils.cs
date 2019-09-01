using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDormitoryManagementSystem.Common
{
    public static class Utils
    {
        public static string GetCurrentDateAsString()
        {
            var now = DateTime.Now;
            return $"{now.Year}{now.Month}{now.Day}{now.Hour}{now.Minute}";
        }
    }
}
