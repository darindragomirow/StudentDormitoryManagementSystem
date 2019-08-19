using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentDormitoryManagementSystem.Areas.Profile
{
    public class ProfileAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Profile";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Profile_default",
                "Profile/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}