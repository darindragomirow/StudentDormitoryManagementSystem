using System.Web.Mvc;

namespace StudentDormitoryManagementSystem.Areas.Profile.Controllers
{

    public class ProfileController : Controller
    {
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}